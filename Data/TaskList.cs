using System;
using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace BlazorServerFirestore.Data
{
    [FirestoreData]
    public class TaskList
    {
        [FirestoreDocumentId]
        public DocumentReference Reference { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public List<Task> Tasks { get; set; }
    }
}
