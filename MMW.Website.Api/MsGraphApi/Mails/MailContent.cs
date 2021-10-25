using Newtonsoft.Json;

namespace MMW.Website.Api.MsGraphApi.Mails
{
    public class MailContent
    {
        [JsonConstructor]
        internal MailContent(MailContentType contentType, string content)
        {
            ContentType = contentType;
            Content = content;
        }

        public MailContentType ContentType { get; }
        public string Content { get; }
    }
}