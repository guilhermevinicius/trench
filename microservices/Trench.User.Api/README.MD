## Trench.User.Api

#### É o microserviço que contem as informações do usuario

### Como executar

Antes de executar o docker-compose, faz o build da imagem
```sh
docker build -f ./docker/Dockerfile . -t trench-user-api
```
Apos fazer o build da imagem, só executar o docker-compose

```sh
docker-compose up -d
```

⚠️ Antes de executar o microserviço, lembra de subir o banco de dados que está na pasta de infrastrutura