FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Cod3rsGrowth.Testes/Cod3rsGrowth.Testes.csproj", "Cod3rsGrowth.Testes/"]
COPY ["Cod3rsGrowth.Web/Cod3rsGrowth.Web.csproj", "Cod3rsGrowth.Web/"]
COPY ["Cod3rsGrowth.Infra/Cod3rsGrowth.Infra.csproj", "Cod3rsGrowth.Infra/"]
COPY ["Cod3rsGrowth.Servico/Cod3rsGrowth.Servico.csproj", "Cod3rsGrowth.Servico/"]
COPY ["Cod3rsGrowth.Dominio/Cod3rsGrowth.Dominio.csproj", "Cod3rsGrowth.Dominio/"]
RUN dotnet restore "./Cod3rsGrowth.Web/Cod3rsGrowth.Web.csproj"
COPY . .
WORKDIR "/src/Cod3rsGrowth.Web"
RUN dotnet build "./Cod3rsGrowth.Web.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cod3rsGrowth.Web.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cod3rsGrowth.Web.dll"]