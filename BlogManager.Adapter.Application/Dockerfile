FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BlogManager.Adapter.Application/BlogManager.Adapter.Application.csproj", "BlogManager.Adapter.Application/"]
RUN dotnet restore "BlogManager.Adapter.Application/BlogManager.Adapter.Application.csproj"
COPY . .
WORKDIR "/src/BlogManager.Adapter.Application"
RUN dotnet build "BlogManager.Adapter.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogManager.Adapter.Application.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogManager.Adapter.Application.dll"]
