using System.Collections.Generic;
using MMW.Website.Api.MsGraphApi.Core;
using MMW.Website.Api.MsGraphApi.Mails;

namespace MMW.Website.Api.MsGraphApi.Users
{
    public partial class User
    {
        public MailFolder GetMailFolder(WellKnownMailFolderName name)
        {
            var ret =
                ApiConnector.Get<MailFolder>($"v1.0/users/{Id}/mailfolders/{name.ToString().ToLower()}");
            if (ret.IsSuccess)
                ret.Content.UserOwner = this;
            return ret.Content;
        }

        public MailFolder GetMailFolderById(string id)
        {
            var ret =
                ApiConnector.Get<MailFolder>($"v1.0/users/{Id}/mailfolders/{id}");
            if (ret.IsSuccess)
                ret.Content.UserOwner = this;
            return ret.Content;
        }

        public MailFolder GetMailFolderByName(string name)
        {
            var ret =
                ApiConnector.Get<List<MailFolder>>($"v1.0/users/{Id}/mailfolders?$filter=displayName eq '{name}'",
                    "value");
            if (!ret.IsSuccess || ret.Content.Count <= 0) return null;
            ret.Content[0].UserOwner = this;
            return ret.Content[0];
        }

        public MailFolder GetMailFolderByPath(string path)
        {
            var folders = path.Split('/');
            MailFolder currentFolder = null;
            for (var i = 0; i < folders.Length; i++)
            {
                if (i == 0)
                {
                    currentFolder = GetMailFolderByName(folders[i]);
                }
                else
                {
                    if (currentFolder.ChildFolderCount == 0)
                        return null;

                    currentFolder = currentFolder.GetChildFolderByName(folders[i]);
                }

                if (currentFolder == null)
                    return null;
            }

            return currentFolder;
        }
    }
}