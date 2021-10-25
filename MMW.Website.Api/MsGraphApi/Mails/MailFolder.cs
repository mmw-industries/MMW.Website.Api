using System.Collections.Generic;
using Newtonsoft.Json;

namespace MMW.Website.Api.MsGraphApi.Mails
{
    public partial class MailFolder
    {
        [JsonConstructor]
        internal MailFolder(string id, string displayName, string parentFolderId, int childFolderCount,
            int unreadItemCount, int totalItemCount, int sizeInBytes, List<MailFolder> childFolders)
        {
            Id = id;
            DisplayName = displayName;
            ParentFolderId = parentFolderId;
            ChildFolderCount = childFolderCount;
            UnreadItemCount = unreadItemCount;
            TotalItemCount = totalItemCount;
            SizeInBytes = sizeInBytes;
            ChildFolders = childFolders;
        }

        public Users.User UserOwner { get; internal set; }
        public string Id { get; }
        public string DisplayName { get; }
        public string ParentFolderId { get; }
        public int ChildFolderCount { get; }
        public int UnreadItemCount { get; }
        public int TotalItemCount { get; }
        public int SizeInBytes { get; }
        public List<MailFolder> ChildFolders { get; set; }
    }
}