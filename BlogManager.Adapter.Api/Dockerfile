FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BlogManager.Adapter.Api/BlogManager.Adapter.Api.csproj", "BlogManager.Adapter.Api/"]
RUN dotnet restore "BlogManager.Adapter.Api/BlogManager.Adapter.Api.csproj"
COPY . .
WORKDIR "/src/BlogManager.Adapter.Api"
RUN dotnet build "BlogManager.Adapter.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogManager.Adapter.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogManager.Adapter.Api.dll"]
