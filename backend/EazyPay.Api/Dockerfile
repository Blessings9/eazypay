# Stage 1: Build and publish
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy backend contents
COPY . .

# Restore and publish only the API project
RUN dotnet restore EazyPay.sln
RUN dotnet publish EazyPay.Api/EazyPay.Api.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EazyPay.Api.dll"]
