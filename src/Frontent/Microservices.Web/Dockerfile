FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.18 as build
WORKDIR /app
EXPOSE 80
COPY Shared/Microservices.Shared/*.csproj Shared/Microservices.Shared/
COPY Frontent/Microservices.Web/*.csproj Frontent/Microservices.Web/
RUN dotnet restore Frontent/Microservices.Web/*.csproj
COPY . .
RUN dotnet publish Frontent/Microservices.Web/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine3.18 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Microservices.Web.dll" ]
