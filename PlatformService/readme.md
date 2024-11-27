# PlatformService

## Build image Docker

bash
```
docker build -t [docker's username]/platformservice .

```

## Run image Docker
bash
```
docker run -e "DOTNET_URLS=http://*:5000" -p 5000:5000 khoizpro7/platformservice

docker run -p 5000:8080 khoizpro7/platformservice

```

## Stop current image Docker
bash
```
docker stop + "container id" 
```

## Run image Docker
bash
```
docker start + "container id"
```
## Push to Docker Hub
bash
```
docker push khoizpro7/platformservice

```
