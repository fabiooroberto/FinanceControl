# Comandos docker para .net

Criar uma imagem da sua aplicação.

## Criar um arquivo Dockerfile na raiz do projeto
Configurar o arquivo

```Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . /wwwroot
RUN dotnet restore /wwwroot
COPY . .
WORKDIR "/src/wwwroot"
RUN dotnet build /wwwroot -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish /wwwroot -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinanceControl.dll"]
```

## Criar uma imagem
```shell
docker build -f {nome-imagem-que-deseja} -f Dockerfile .
docker build -t finance -f Dockerfile .
```

## Lista as imagens
```shell
docker images
```

## Criar um container
```shell
docker create --name {nome-container} {nome-imagem-que-deseja}
docker create --name finance-api finance
```

## Rodar uma imagem e criar container
```shell
docker run -d -p 5000:8080/tcp --name finance-api finance:latest
```

## Lista os containers
```shell
docker ps -a
```

## Iniciar um container
```shell
docker start finance-api
```

## Parar um container
```shell
docker stop finance-api
```

## Excluir um container
```shell
docker rm core-counter
```
## Criar um arquivo docker-compose.yml
```yml
version: "3.9"
services: 
    webapi:
        image: finance
        container_name: finance_api
        environment:
            - ASPNETCORE_HTTP_PORTS=5000
    build: .
    ports:
        - "5000:5000"
```

## Executar o docker compose
Vai subir (up) o container e rodar em background (-d)
```shell
docker compose up -d
```

## Deburrar o docker compose
Vai subir (up) o container e rodar em background (-d)
```shell
docker compose down
```
