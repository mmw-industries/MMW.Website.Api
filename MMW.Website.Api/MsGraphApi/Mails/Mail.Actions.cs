using MMW.Website.Api.MsGraphApi.Core;
using MMW.Website.Api.MsGraphApi.Users;

namespace MMW.Website.Api.MsGraphApi.Mails
{
    public partial class Mail
    {
        internal static bool Send(User user, MailToSend mail)
        {
            var ret = 
                ApiConnector.Post<string>($"v1.0/users/{user.Id}/sendMail", new { message = mail });
            return ret.IsSuccess;
        }

        internal static Mail ById(User user, string mailId)
        {
            var ret =
                ApiConnector.Get<Mail>($"v1.0/users/{user.Id}/messages/{mailId}");
            if (ret.IsSuccess)
                ret.Content.UserOwner = user;
            return ret.Content;
        }

        public bool Delete()
        {
            var deleteFolder = UserOwner.GetMailFolder(WellKnownMailFolderName.DeletedItems);
            return deleteFolder != null && Move(deleteFolder);
        }

        public bool MarkAsRead()
        {
            var ret =
                ApiConnector.Patch<Mail>($"v1.0/users/{UserOwner.Id}/messages/{Id}",
                    new
                    {
                        isRead = true
                    });
            if (ret.IsSuccess) Update(ret.Content);
            return ret.IsSuccess;
        }

        private void Update(Mail mail)
        {
            if (ParentFolderId != mail.ParentFolderId)
                ParentFolderId = mail.ParentFolderId;

            if (IsRead != mail.IsRead)
                IsRead = mail.IsRead;
        }

        public bool Move(MailFolder folder)
        {
            if (folder == null)
                return false;
            return Move(folder.Id);
        }

        public bool Move(string destinationFolderId)
        {
            var ret =
                ApiConnector.Post<Mail>($"v1.0/users/{UserOwner.Id}/messages/{Id}/move",
                    new
                    {
                        destinationId = destinationFolderId
                    });
            if (ret.IsSuccess) Update(ret.Content);
            return ret.IsSuccess;
        }
    }
}