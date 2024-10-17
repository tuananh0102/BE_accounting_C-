#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MISA.Web04.Api/MISA.Web04.Api.csproj", "MISA.Web04.Api/"]
COPY ["MISA.Web04.Core/MISA.Web04.Core.csproj", "MISA.Web04.Core/"]
COPY ["MISA.Web04.Infrastructure/MISA.Web04.Infrastructure.csproj", "MISA.Web04.Infrastructure/"]
RUN dotnet restore "MISA.Web04.Api/MISA.Web04.Api.csproj"
COPY . .
WORKDIR "/src/MISA.Web04.Api"
RUN dotnet build "MISA.Web04.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MISA.Web04.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MISA.Web04.Api.dll"]