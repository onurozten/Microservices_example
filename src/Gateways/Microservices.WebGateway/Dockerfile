FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.18 as build
WORKDIR /app
EXPOSE 80
COPY Gateways/Microservices.WebGateway/*.csproj Gateways/Microservices.WebGateway/
RUN dotnet restore Gateways/Microservices.WebGateway/*.csproj
COPY . .
RUN dotnet publish Gateways/Microservices.WebGateway/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine3.18 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Microservices.WebGateway.dll" ]
