FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /
COPY ["../Directory.Build.props", ""]
COPY ["../src/Trench.Notification.Api/Trench.Notification.Api.csproj", "src/Trench.Notification.Api/"]
COPY ["../src/Trench.Notification.Application/Trench.Notification.Application.csproj", "src/Trench.Notification.Application/"]
COPY ["../src/Trench.Notification.CrossCutting/Trench.Notification.CrossCutting.csproj", "src/Trench.Notification.CrossCutting/"]
COPY ["../src/Trench.Notification.Domain/Trench.Notification.Domain.csproj", "src/Trench.Notification.Domain/"]
COPY ["../src/Trench.Notification.Persistence/Trench.Notification.Persistence.csproj", "src/Trench.Notification.Persistence/"]
COPY ["../src/Trench.Notification.MessageQueue/Trench.Notification.MessageQueue.csproj", "src/Trench.Notification.MessageQueue/"]
RUN dotnet restore src/Trench.Notification.Api/Trench.Notification.Api.csproj

COPY . .
WORKDIR "src/Trench.Notification.Api"
RUN dotnet build "Trench.Notification.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Trench.Notification.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_HTTP_PORTS=80
ENTRYPOINT ["dotnet", "Trench.Notification.Api.dll"]