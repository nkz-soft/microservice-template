# Kubernetes deployment

This folder contains the files required to deploy to a Kubernetes cluster.

## Install microservice-template using Helm

### Install infrastructural components

* For Linux and locally installed k8s cluster
```bash
cd ./deployment/k8s/infrastructure/
./install.sh
```
* For Windows and Mac: **Coming soon**

### Install service
```bash
cd ./deployment/k8s/.helm/
helm upgrade template-service -i .
```
