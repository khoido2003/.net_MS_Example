# Kubernetes setup

## Setup acme.com for localhost
add acme.com to the file below
bash
```
#localhost
127.0.0.1 acme.com

\Windows\System32\drivers\etc\hosts.
```

## Check version

bash

```
kubectl version
```

## Deploy

bash

```
kubectl apply -f platforms-depl.yaml
```

## Start nginx ingress controller
bash
```
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.12.0-beta.0/deploy/static/provider/cloud/deploy.yaml
```

## Check deployment

bash

```
kubectl get deployments
```

## Check pod

bash

```
kubectl get pods
```

## Check service

bash

```
kubectl get services
```

## Check pod logs

bash

```
kubectl logs platform-depl-6c8d6874c7-2pdm4
```

## Delete all pods
bash
```
kubectl delete pods --all -n <namespace>

kubectl delete pods --all -n default

kubectl delete all --all -n <namespace>
```

## Delete deployment

bash

```
kubectl delete deployment platform-depl
```

## Scale down pods
bash
```
kubectl scale deployment --all --replicas=0 -n <namespace>

kubectl scale deployment --all --replicas=0 -n default
```

## Scale down nginx ingress controller
bash
```
kubectl get deployments -n ingress-nginx

kubectl scale deployment ingress-nginx-controller --replicas=0 -n ingress-nginx
```

## Apply and Update service

bash

```
kubectl apply -f <service-config-file>.yaml
```

## Restart service

bash

```
kubectl rollout restart deployment platform-depl
```

## Get namespace
bash
```
kubectl get namespace

```

## Get pods from a namespace
bash
```
kubectl get pods --namespace=ingress-nginx
```

## See the ingress-nginx
bash
```
kubectl get ingress --all-namespaces
```

## Get storageclass
bash
```
kubectl get storageclass
```

## Get persistent volume Claim
bash
```
kubectl get pvc
```

1. Persistent Volume (PV)

A Persistent Volume is a piece of storage in the cluster that has been provisioned by an administrator or dynamically by a StorageClass. PVs are independent of the lifecycle of any pod that uses them, meaning the data stored is preserved even if a pod is deleted.
Key Features:

    Pre-provisioned or dynamically provisioned.
    Cluster-wide resource.
    Defined in a YAML file and managed by the Kubernetes control plane.

Access Modes:

    ReadWriteOnce: Mounted as read-write by a single node.
    ReadOnlyMany: Mounted as read-only by multiple nodes.
    ReadWriteMany: Mounted as read-write by multiple nodes.

2. Persistent Volume Claim (PVC)

A Persistent Volume Claim is a request for storage by a user. It is similar to a pod requesting a specific amount of CPU or memory. PVCs are bound to PVs, either statically or dynamically.
Key Features:

    Users request storage through PVCs without needing to manage the underlying PV details.
    PVCs define the required storage size, access modes, and optionally the StorageClass.

3. StorageClass (SC)

A StorageClass provides a way to describe the "class" of storage you want. It abstracts the details of the storage type (e.g., SSD, HDD, cloud storage) and allows dynamic provisioning of PVs.
Key Features:

    Automates the creation of PVs when a PVC requests it.
    Supports parameters like storage type, zone, replication, etc.
    Different StorageClasses can be created for different types of storage needs (e.g., faster SSDs vs. cheaper HDDs).

## Create Kubernetes secret
bash
```
kubectl create secret generic mssql --from-literal=SA_PASSWORD="password"

```

## Delete Kubernetes secret
bash
```
kubectl delete secret mysql-secret
```

## Update Kubernetes secret
bash
```
kubectl create secret generic mysql-secret --from-literal=MYSQL_ROOT_PASSWORD=newpassword --dry-run=client -o yaml | kubectl apply -f 

```

## Get the ingress controller
bash 
```
kubectl get pods -n ingress-nginx
```

## Get the ingress resouces
bash
```
kubectl get ingress
```
