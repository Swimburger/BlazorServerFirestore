using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace FirestoreConsole
{
    class Program
    {
        // code borrowed from https://cloud.google.com/firestore/docs/quickstart-servers#cloud-console
        public static async Task Main(string[] args)
        {
            var projectName = "blazor-sample";
            var authFilePath = "PATH/TO/blazor-sample-auth.json";
            // environment variable could be configured differently, but for the sample simply hardcode it
            // the Firestore library expects this environment variable to be set
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", authFilePath);
            FirestoreDb firestoreDb = FirestoreDb.Create(projectName);

            // this section creates two document in the users-collection, alovelace and aturing
            CollectionReference usersCollection = firestoreDb.Collection("users");
            DocumentReference docRef = usersCollection.Document("alovelace");
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "First", "Ada" },
                { "Last", "Lovelace" },
                { "Born", 1815 }
            };
            await docRef.SetAsync(user);
  
            docRef = usersCollection.Document("aturing");
            user = new Dictionary<string, object>
            {
                { "First", "Alan" },
                { "Middle", "Mathison" },
                { "Last", "Turing" },
                { "Born", 1912 }
            };
            await docRef.SetAsync(user);

            // this section will fetch all users from the users-collection and print them to the console
            QuerySnapshot snapshot = await usersCollection.GetSnapshotAsync();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Console.WriteLine("User: {0}", document.Id);
                Dictionary<string, object> documentDictionary = document.ToDictionary();
                Console.WriteLine("First: {0}", documentDictionary["First"]);
                if (documentDictionary.ContainsKey("Middle"))
                {
                    Console.WriteLine("Middle: {0}", documentDictionary["Middle"]);
                }
                Console.WriteLine("Last: {0}", documentDictionary["Last"]);
                Console.WriteLine("Born: {0}", documentDictionary["Born"]);
                Console.WriteLine();
            }

            // all users will be fetched and send to the lambda callback, when users-collection is modified the changes will be send here in real-time
            // not only the change will be send when collection is modified, the entire collection will be returne
            FirestoreChangeListener firestoreChangeListener = usersCollection
                .Listen((snapshot) =>
                {
                    foreach (DocumentSnapshot document in snapshot.Documents)
                    {
                        Console.WriteLine("User: {0}", document.Id);
                        Dictionary<string, object> documentDictionary = document.ToDictionary();
                        Console.WriteLine("First: {0}", documentDictionary["First"]);
                        Console.WriteLine("Last: {0}", documentDictionary["Last"]);
                        Console.WriteLine();
                    }
                });

            // give some time for the 'Listen' function to be invoked
            await Task.Delay(2000);

            Console.WriteLine("Enter first name:");
            var firstName = Console.ReadLine();

            Console.WriteLine("Enter last name:");
            var lastName = Console.ReadLine();
            Console.WriteLine();

            user = new Dictionary<string, object>
            {
                { "First", firstName },
                { "Last", lastName }
            };
            await usersCollection.AddAsync(user);
            
            // give some time for the 'Listen' function to be invoked
            await Task.Delay(2000);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            await firestoreChangeListener.StopAsync();
        }
    }
}