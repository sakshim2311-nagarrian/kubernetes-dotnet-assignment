# Kubernetes Deployment Assignment

## Project Overview

This repository contains Kubernetes deployment configuration for a containerized .NET Service API application.

The application is deployed on a Kubernetes cluster with:

- API Microservice
- PostgreSQL Database
- Kubernetes Deployment
- Kubernetes Service
- Ingress
- Persistent Storage
- Horizontal Pod Autoscaler
- Rolling Updates


## Repository URL

GitHub Repository:

https://github.com/sakshim2311-nagarrian/kubernetes-dotnet-assignment


## Docker Image

Docker Hub Image:

https://hub.docker.com/r/sakshim2311/serviceapi


Example:

docker pull sakshim2311/serviceapi:v3


## Kubernetes Objects Deployed

The following Kubernetes objects are included:

- Deployment
- ReplicaSet
- Pods
- Service (LoadBalancer)
- Ingress
- ConfigMap
- Secret
- PersistentVolumeClaim
- HorizontalPodAutoscaler
- PostgreSQL Database Deployment


## Application URL

Service API URL:

http://<external-ip>/index.html


Example:

http://34.134.13.139/api/records


This endpoint retrieves records from the backend database.


## Kubernetes Demo Recording

Video:

https://1drv.ms/v/c/cd0c84743e83b796/IQBv5G8ePrKdT5DobVmbsjnMAVb8yfSZ1DNp5HMjjAzg4ZU?e=GFGTqx


The recording demonstrates:

- All Kubernetes objects deployed and running
- API call retrieving records from database
- API pod deletion and automatic recreation
- Database pod deletion and recovery with persistent data
- Deployment rollout strategy
- Self healing behaviour
- Horizontal Pod Autoscaling

## Author

Sakshi Mittal