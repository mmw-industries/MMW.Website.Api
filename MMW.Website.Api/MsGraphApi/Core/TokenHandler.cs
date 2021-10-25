using System;
using Microsoft.Identity.Client;

namespace MMW.Website.Api.MsGraphApi.Core
{
    internal static class TokenHandler
    {
        #region Private Deklaration

        private static AuthenticationResult CurrentAuthentication;

        #endregion

        #region Public Deklaration

        public static string CurrentAccessToken => GetCurrentAccessToken();

        #endregion

        #region Access Token

        private static string GetCurrentAccessToken()
        {
            while (true)
            {
                if (CurrentAuthentication != null && CurrentAuthentication.ExpiresOn > DateTime.UtcNow)
                    return CurrentAuthentication.AccessToken;

                // we have no token or current token is expired
                // so we need to get a new one
                CurrentAuthentication = GetNewAccessToken();
            }
        }

        private static AuthenticationResult GetNewAccessToken()
        {
            // Even if this is a console application here, a daemon application is a confidential client application
            var app =
                ConfidentialClientApplicationBuilder.Create(Settings.ClientId)
                    .WithClientSecret(Settings.ClientSecret)
                    .WithAuthority(new Uri(Settings.Authority))
                    .Build();

            // With client credentials flows the scopes is ALWAYS of the shape "resource/.default", as the 
            // application permissions need to be set statically (in the portal or by PowerShell), and then granted by
            // a tenant administrator. 
            string[] scopes = { $"{Settings.ApiUrl}.default" };

            AuthenticationResult result = null;
            try
            {
                result = app.AcquireTokenForClient(scopes)
                    .ExecuteAsync().GetAwaiter().GetResult();
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011"))
            {
                // Invalid scope. The scope has to be of the form "https://resourceurl/.default"
                // Mitigation: change the scope to be as expected
                Console.WriteLine("MS Graph Api: Scope provided is not supported");
            }

            return result;
        }

        #endregion
    }
}