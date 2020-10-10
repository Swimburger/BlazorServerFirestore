#!/bin/bash
# create a project to hold cloud resources
gcloud projects create blazor-sample
# switch to this project, this will make sure subsequent commands will use this project
gcloud config set project blazor-sample

# Firestore requires an App Engine in your project
gcloud app create --region=us-east1
# create a Firestore in native mode
gcloud firestore databases create --region=us-east1

# create a service account to authenticate against the Firestore
gcloud iam service-accounts create blazor-sample-service-account
# give service account owner-role of the project
gcloud projects add-iam-policy-binding blazor-sample --member "serviceAccount:blazor-sample-service-account@blazor-sample.iam.gserviceaccount.com" --role "roles/owner"
# create a json-authentication file to authenticate as service account
gcloud iam service-accounts keys create blazor-sample-auth.json --iam-account blazor-sample-service-account@blazor-sample.iam.gserviceaccount.com
# download the json-authentication file to your machine
cloudshell download blazor-sample-auth.json