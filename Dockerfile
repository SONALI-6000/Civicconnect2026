FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["CivicConnect.csproj", "./"]
RUN dotnet restore

COPY . .
RUN dotnet publish "CivicConnect.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "CivicConnect.dll"]