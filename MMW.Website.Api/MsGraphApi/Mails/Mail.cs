using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MMW.Website.Api.MsGraphApi.Mails
{
    public partial class Mail
    {
        internal Mail(string subject, MailContent body, List<EmailContact> toRecipients)
        {
            Subject = subject;
            Body = Body;
            ToRecipients = toRecipients;
        }
        
        [JsonConstructor]
        private Mail(string id, DateTime createdDateTime, DateTime sendDateTime, DateTime receivedDateTime,
            bool hasAttachments, bool isRead, bool isDraft, bool isReadReceiptRequested, string subject,
            string bodyPreview, MailContent body, EmailContact sender, EmailContact from,
            List<EmailContact> toRecipients, List<EmailContact> ccRecipients, List<EmailContact> bccRecipients,
            string parentFolderId, string conversationId, string conversationIndex)
        {
            Id = id;
            CreatedDateTime = createdDateTime;
            SendDateTime = sendDateTime;
            ReceivedDateTime = receivedDateTime;
            HasAttachments = hasAttachments;
            IsRead = isRead;
            IsDraft = isDraft;
            IsReadReceiptRequested = isReadReceiptRequested;
            Subject = subject;
            BodyPreview = bodyPreview;
            Body = body;
            Sender = sender;
            From = from;
            ToRecipients = toRecipients;
            CcRecipients = ccRecipients;
            BccRecipients = bccRecipients;
            ParentFolderId = parentFolderId;
            ConversationId = conversationId;
            ConversationIndex = conversationIndex;
        }

        public Users.User UserOwner { get; internal set; }
        public string Id { get; }
        public DateTime CreatedDateTime { get; }
        public DateTime SendDateTime { get; }
        public DateTime ReceivedDateTime { get; }
        public bool HasAttachments { get; }
        public bool IsRead { get; set; }
        public bool IsDraft { get; }
        public bool IsReadReceiptRequested { get; }
        public string Subject { get; }
        public string BodyPreview { get; }
        public MailContent Body { get; }
        public MailContent UniqueBody { get; }
        public EmailContact Sender { get; }
        public EmailContact From { get; }
        public List<EmailContact> ToRecipients { get; }
        public List<EmailContact> CcRecipients { get; }
        public List<EmailContact> BccRecipients { get; }
        public string ParentFolderId { get; set; }
        public string ConversationId { get; }
        public string ConversationIndex { get; }
    }
}