using System.Globalization;

namespace MMW.Website.Api.MsGraphApi
{
    public static class Settings
    {
        /// <summary>
        ///     instance of Azure AD, for example public Azure or a Sovereign cloud (Azure China, Germany, US government, etc ...)
        /// </summary>
        private static string Instance { get; } = "https://login.microsoftonline.com/{0}";

        /// <summary>
        ///     Graph API endpoint, could be public Azure (default) or a Sovereign cloud (US government, etc ...)
        /// </summary>
        internal static string ApiUrl { get; set; } = "https://graph.microsoft.com/";

        /// <summary>
        ///     The Tenant is:
        ///     - either the tenant ID of the Azure AD tenant in which this application is registered (a guid)
        ///     or a domain name associated with the tenant
        ///     - or 'organizations' (for a multi-tenant application)
        /// </summary>
        public static string Tenant { get; set; }

        /// <summary>
        ///     Guid used by the application to uniquely identify itself to Azure AD
        /// </summary>
        public static string ClientId { internal get; set; }

        /// <summary>
        ///     URL of the authority
        /// </summary>
        public static string Authority => string.Format(CultureInfo.InvariantCulture, Instance, Tenant);

        /// <summary>
        ///     Client secret (application password)
        /// </summary>
        /// Daemon applications can authenticate with AAD through two mechanisms: ClientSecret
        /// (which is a kind of application password: this property)
        /// or a certificate previously shared with AzureAD during the application registration
        /// (and identified by the CertificateName property belows)
        public static string ClientSecret { internal get; set; }
    }
}