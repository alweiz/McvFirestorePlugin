using Google.Cloud.Firestore;

namespace McvFirestorePlugin.Entities
{
    [FirestoreData]
    public class YouTubeLiveChatMessage
    {
        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty("userIconUrl")]
        public string UserIconUrl { get; set; }

        [FirestoreProperty("userDisplayName")]
        public string UserDisplayName { get; set; }

        [FirestoreProperty("messageId")]
        public string MessageId { get; set; }

        [FirestoreProperty("text")]
        public string Text { get; set; }

        [FirestoreProperty("messageType")]
        public string MessageType { get; set; }

        [FirestoreProperty("postedAt")]
        public Timestamp PostedAt { get; set; }

        [FirestoreDocumentCreateTimestamp]
        public Timestamp CreatedAt { get; set; }

        [FirestoreDocumentUpdateTimestamp]
        public Timestamp UpdatedAt { get; set; }

        [FirestoreDocumentReadTimestamp]
        public Timestamp ReadAt { get; set; }
    }
}
