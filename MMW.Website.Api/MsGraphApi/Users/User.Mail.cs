using MMW.Website.Api.MsGraphApi.Mails;

namespace MMW.Website.Api.MsGraphApi.Users
{
    public partial class User
    {
        public bool SendMail(MailToSend mail)
        {
            return Mails.Mail.Send(this, mail);
        }
        
        public Mail GetMailById(string mailId)
        {
            return Mails.Mail.ById(this, mailId);
        }
    }
}