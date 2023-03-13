# WorkerTest

### Create Docker Image
```
docker build -t workertest .
```

### Pushing image to DockerHub
```
docker push <user>/workertest:latest
```

### Create Deployment on cluster Kubernetes
```
kubectl apply -f <FolderProject>\WorkerTest\k8s\
```
