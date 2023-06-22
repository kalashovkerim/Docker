#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["turbo_azure/turbo_azure.csproj", "turbo_azure/"]
RUN dotnet restore "turbo_azure/turbo_azure.csproj"
COPY . .
WORKDIR "/src/turbo_azure"
RUN dotnet build "turbo_azure.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "turbo_azure.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "turbo_azure.dll"]