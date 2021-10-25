using Newtonsoft.Json;

namespace MMW.Website.Api.MsGraphApi.Mails
{
    public class EmailContact
    {
        [JsonConstructor]
        internal EmailContact(EmailAddress emailAddress)
        {
            EmailAddress = emailAddress;
        }

        public EmailAddress EmailAddress { get; }
    }
}