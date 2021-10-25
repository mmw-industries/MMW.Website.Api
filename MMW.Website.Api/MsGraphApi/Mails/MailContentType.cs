using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MMW.Website.Api.MsGraphApi.Mails
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MailContentType
    {
        Html,
        Text
    }
}