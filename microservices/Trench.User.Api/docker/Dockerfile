FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /
COPY ["../Directory.Build.props", ""]
COPY ["../src/Trench.User.Api/Trench.User.Api.csproj", "src/Trench.User.Api/"]
COPY ["../src/Trench.User.Application/Trench.User.Application.csproj", "src/Trench.User.Application/"]
COPY ["../src/Trench.User.CrossCutting/Trench.User.CrossCutting.csproj", "src/Trench.User.CrossCutting/"]
COPY ["../src/Trench.User.Domain/Trench.User.Domain.csproj", "src/Trench.User.Domain/"]
COPY ["../src/Trench.User.Persistence/Trench.User.Persistence.csproj", "src/Trench.User.Persistence/"]
COPY ["../src/Trench.User.MessageQueue/Trench.User.MessageQueue.csproj", "src/Trench.User.MessageQueue/"]
COPY ["../src/Trench.User.Storage/Trench.User.Storage.csproj", "src/Trench.User.Storage/"]
RUN dotnet restore src/Trench.User.Api/Trench.User.Api.csproj

COPY . .
WORKDIR "src/Trench.User.Api"
RUN dotnet build "Trench.User.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Trench.User.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_HTTP_PORTS=80
ENTRYPOINT ["dotnet", "Trench.User.Api.dll"]