
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY src/NotificationsAPI/NotificationsAPI.csproj src/NotificationsAPI/
RUN dotnet restore src/NotificationsAPI/NotificationsAPI.csproj
COPY src/NotificationsAPI/ src/NotificationsAPI/
RUN dotnet publish src/NotificationsAPI/NotificationsAPI.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "NotificationsAPI.dll"]
