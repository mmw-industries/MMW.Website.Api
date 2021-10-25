using Newtonsoft.Json;

namespace MMW.Website.Api.MsGraphApi.Users
{
    public partial class User
    {
        #region Constructor

        [JsonConstructor]
        private User(string id, string displayName, string givenName, string userPrincipalName, string surName,
            string jobTitle, string mail, string mobilePhone)
        {
            Id = id;
            DisplayName = displayName;
            GivenName = givenName;
            UserPrincipalName = userPrincipalName;
            SurName = surName;
            JobTitle = jobTitle;
            Mail = mail;
            MobilePhone = mobilePhone;
        }

        #endregion

        #region Public Deklaration

        public string Id { get; }
        public string DisplayName { get; }
        public string GivenName { get; }
        public string UserPrincipalName { get; }
        public string SurName { get; }
        public string JobTitle { get; }
        public string Mail { get; }
        public string MobilePhone { get; }

        #endregion
    }
}