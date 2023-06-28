FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Vehicle/Vehicle.Api/Vehicle.Api.csproj", "Services/Vehicle/Vehicle.Api/"]
COPY ["Services/Vehicle/Vehicle.Contract/Vehicle.Contract.csproj", "Services/Vehicle/Vehicle.Contract/"]
COPY ["Infrastructure/BaseTypes/BaseTypes.csproj", "Infrastructure/BaseTypes/"]
COPY ["Services/Vehicle/Vehicle.Svc/Vehicle.Svc.csproj", "Services/Vehicle/Vehicle.Svc/"]
COPY ["Services/Vehicle/Vehicle.Api.Module/Vehicle.Api.Module.csproj", "Services/Vehicle/Vehicle.Api.Module/"]
COPY ["Services/Vehicle/Vehicle.Api/coordinates.txt", "Services/Vehicle/Vehicle.Api/"]
RUN dotnet restore "Services/Vehicle/Vehicle.Api/Vehicle.Api.csproj"
COPY . .
WORKDIR "/src/Services/Vehicle/Vehicle.Api"
RUN dotnet build "Vehicle.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vehicle.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vehicle.Api.dll"]
