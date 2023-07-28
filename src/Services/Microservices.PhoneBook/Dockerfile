FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.18 as build
WORKDIR /app
EXPOSE 80
COPY Shared/Microservices.Shared/*.csproj Shared/Microservices.Shared/
COPY Services/Microservices.PhoneBook/*.csproj Services/Microservices.PhoneBook/
RUN dotnet restore Services/Microservices.PhoneBook/*.csproj
COPY . .
RUN dotnet publish Services/Microservices.PhoneBook/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine3.18 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Microservices.PhoneBook.dll" ]
