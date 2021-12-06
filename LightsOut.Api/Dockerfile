#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["../LightsOut.Api/LightsOut.Api.csproj", "LightsOut.Api/"]
COPY ["../LightsOut.Domain/LightsOut.Domain.csproj", "LightsOut.Domain/"]
COPY ["../LightsOut.Persistence/LightsOut.Persistence.csproj", "LightsOut.Persistence/"]
COPY ["../LightsOut.Application/LightsOut.Application.csproj", "LightsOut.Application/"]
RUN dotnet restore "LightsOut.Api/LightsOut.Api.csproj"
COPY . .
WORKDIR "/src/LightsOut.Api"
RUN dotnet build "LightsOut.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LightsOut.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LightsOut.Api.dll"]
