#!/bin/bash
PROJECT="blazor-sample"
SERVICE_ACCOUNT="blazor-sample-service-account"
AUTH_FILE="blazor-sample-auth.json"
REGION="us-east1"

# create a project to hold cloud resources
gcloud projects create $PROJECT
# switch to this project, this will make sure subsequent commands will use this project
gcloud config set project $PROJECT

# Firestore requires an App Engine in your project
gcloud app create --region=$REGION
# enable app engine API, required by Firestore
gcloud services enable appengine.googleapis.com
# create a Firestore in native mode
gcloud firestore databases create --region=$REGION

# create a service account to authenticate against the Firestore
gcloud iam service-accounts create $SERVICE_ACCOUNT
# give service account owner-role of the project
gcloud projects add-iam-policy-binding $PROJECT --member "serviceAccount:$SERVICE_ACCOUNT@$PROJECT.iam.gserviceaccount.com" --role "roles/owner"

# create a json-authentication file to authenticate as service account
gcloud iam service-accounts keys create $AUTH_FILE --iam-account "$SERVICE_ACCOUNT@$PROJECT.iam.gserviceaccount.com"
# download the json-authentication file to your machine
cloudshell download $AUTH_FILE