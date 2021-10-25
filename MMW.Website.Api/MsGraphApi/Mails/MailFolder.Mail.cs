using System.Collections.Generic;
using MMW.Website.Api.MsGraphApi.Core;

namespace MMW.Website.Api.MsGraphApi.Mails
{
    public partial class MailFolder
    {
        public List<Mail> GetMails(int limit = 250, string select = "*")
        {
            var ret =
                ApiConnector.Get<List<Mail>>(
                    $"v1.0/users/{UserOwner.Id}/mailfolders/{Id}/messages?$top=250&$select={select}", "value");
            if (!ret.IsSuccess) return ret.Content;
            foreach (var item in ret.Content)
                item.UserOwner = UserOwner;
            return ret.Content;
        }

        public Mail GetMailById(string mailId)
        {
            return Mail.ById(UserOwner, mailId);
        }
    }
}