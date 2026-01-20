
# ------------------------ Build stage ------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ENV DOTNET_CLI_HOME=/tmp
ENV NUGET_PACKAGES=/root/.nuget/packages

COPY src/NotificationsAPI/NotificationsAPI.csproj src/NotificationsAPI/
RUN dotnet restore src/NotificationsAPI/NotificationsAPI.csproj -p:DisableImplicitNuGetFallbackFolder=true

COPY src/NotificationsAPI/ src/NotificationsAPI/
RUN dotnet publish src/NotificationsAPI/NotificationsAPI.csproj -c Release -o /app/publish --no-restore \
    -p:DisableImplicitNuGetFallbackFolder=true

# ------------------------ Runtime stage ------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "NotificationsAPI.dll"]
