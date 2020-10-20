# Blazor Server Firestore Integration
This repository contains the source for the Twilio walkthrough "[Building Real-Time Applications with Blazor Server and Firestore](https://www.twilio.com/blog/building-real-time-applications-with-blazor-server-and-firestore)".
There are three projects:
- [FirestoreConsole](./FirestoreConsole): a .NET Core console application to test & verify integration with Firestore in .NET, used in Twilio walkthrough
- [BlazorFirestoreMinimal](./BlazorFirestoreMinimal): Integrating Firestore with Blazor Server to build a real-time UI, used in Twilio walkthrough
- [BlazorFirestoreSamples](./BlazorFirestoreSamples): Enhanced UI on top of the BlazorFirestoreMinimal & additionally a real-time To Do list application

To create the necessary resources to run Firestore in GCP, run the commands from [gcp-commands.sh](./gcp-commands.sh)
