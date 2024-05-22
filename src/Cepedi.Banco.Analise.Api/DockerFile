#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Cepedi.Banco.Analise.Api/Cepedi.Banco.Analise.Api.csproj", "src/Cepedi.Banco.Analise.Api/"]
COPY ["src/Cepedi.Banco.Analise.IoC/Cepedi.Banco.Analise.IoC.csproj", "src/Cepedi.Banco.Analise.IoC/"]
COPY ["src/Cepedi.Banco.Analise.Compartilhado/Cepedi.Banco.Analise.Compartilhado.csproj", "src/Cepedi.Banco.Analise.Compartilhado/"]
COPY ["src/Cepedi.Banco.Analise.Dados/Cepedi.Banco.Analise.Dados.csproj", "src/Cepedi.Banco.Analise.Dados/"]
COPY ["src/Cepedi.Banco.Analise.Dominio/Cepedi.Banco.Analise.Dominio.csproj", "src/Cepedi.Banco.Analise.Dominio/"]
RUN dotnet restore "./src/Cepedi.Banco.Analise.Api/Cepedi.Banco.Analise.Api.csproj"
COPY . .
WORKDIR "/src/src/Cepedi.Banco.Analise.Api"
RUN dotnet build "./Cepedi.Banco.Analise.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cepedi.Banco.Analise.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cepedi.Banco.Analise.Api.dll"]