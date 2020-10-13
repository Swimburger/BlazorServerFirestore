using Google.Cloud.Firestore;

namespace BlazorServerFirestore
{
    [FirestoreData]
    public class Task
    {
        [FirestoreProperty]
        public string Content { get; set; }

        [FirestoreProperty("Done")]
        public bool IsDone { get; set; }
    }
}