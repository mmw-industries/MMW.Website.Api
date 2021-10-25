using System.Collections.Generic;
using MMW.Website.Api.MsGraphApi.Core;

namespace MMW.Website.Api.MsGraphApi.Mails
{
    public partial class MailFolder
    {
        public bool LoadChildFolders(int limit = 250)
        {
            var ret =
                ApiConnector.Get<List<MailFolder>>(
                    $"v1.0/users/{UserOwner.Id}/mailfolders/{Id}/childFolders?$top={limit}", "value");
            if (ret.IsSuccess)
                ChildFolders = ret.Content;
            return ret.IsSuccess;
        }

        public MailFolder GetChildFolderByName(string name)
        {
            var ret =
                ApiConnector.Get<List<MailFolder>>(
                    $"v1.0/users/{UserOwner.Id}/mailfolders/{Id}/childFolders?$filter=displayName eq '{name}'",
                    "value");
            if (!ret.IsSuccess || ret.Content.Count <= 0) return null;
            ret.Content[0].UserOwner = UserOwner;
            return ret.Content[0];
        }
    }
}