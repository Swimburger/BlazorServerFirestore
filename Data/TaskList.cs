using System;
using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace BlazorServerFirestore.Data
{
    [FirestoreData]
    public class TaskList
    {
        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public List<Task> Tasks { get; set; }
    }
}
