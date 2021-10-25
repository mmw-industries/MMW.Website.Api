using Newtonsoft.Json;

namespace MMW.Website.Api.MsGraphApi.Mails
{
    public class EmailAddress
    {
        [JsonConstructor]
        internal EmailAddress(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public string Name { get; }
        public string Address { get; }
    }
}