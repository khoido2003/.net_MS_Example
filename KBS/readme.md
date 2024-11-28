# Kubernetes setup

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

## Delete deployment
bash
```
kubectl delete deployment platform-depl
```

## Apply and Update service
bash
```
kubectl apply -f <service-config-file>.yaml
```
