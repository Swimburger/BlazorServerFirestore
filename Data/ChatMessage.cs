using Google.Cloud.Firestore;

namespace BlazorServerFirestore
{
    [FirestoreData]
    public class ChatMessage
    {
        [FirestoreProperty]
        public string Message { get; set; }

        [FirestoreProperty, ServerTimestamp]
        public Timestamp CreatedAt { get; set; }
    }
}