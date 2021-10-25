using System.Collections.Generic;
using System.Linq;

namespace MMW.Website.Api.MsGraphApi.Mails
{
    public class MailToSend
    {
        public MailToSend(string subject, string body, List<EmailContact> to, List<EmailContact> cc = null,
            List<EmailContact> bcc = null)
        {
            Subject = subject;
            ToRecipients = to;
            CcRecipients = cc;
            BccRecipients = bcc;
            Body = new MailContent(MailContentType.Html, body);
        }

        public MailToSend(string subject, string body, string to, string cc = "", string bcc = "")
        {
            Subject = subject;
            ToRecipients = StringToEmailContacts(to);
            CcRecipients = StringToEmailContacts(cc);
            BccRecipients = StringToEmailContacts(bcc);
            Body = new MailContent(MailContentType.Html, body);
        }

        public string Subject { get; }
        public List<EmailContact> ToRecipients { get; }
        public List<EmailContact> CcRecipients { get; }
        public List<EmailContact> BccRecipients { get; }
        public MailContent Body { get; }

        private List<EmailContact> StringToEmailContacts(string value)
        {
            return string.IsNullOrEmpty(value)
                ? new List<EmailContact>()
                : value.Split(";").Select(mail => new EmailContact(new EmailAddress("", mail))).ToList();
        }
    }
}