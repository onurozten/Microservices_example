FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.18 as build
WORKDIR /app
EXPOSE 80
COPY Shared/Microservices.Shared/*.csproj Shared/Microservices.Shared/
COPY Services/Microservices.Reporter/*.csproj Services/Microservices.Reporter/
RUN dotnet restore Services/Microservices.Reporter/*.csproj
COPY . .
RUN dotnet publish Services/Microservices.Reporter/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine3.18 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Microservices.Reporter.dll" ]
