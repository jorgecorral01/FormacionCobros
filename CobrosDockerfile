FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src

COPY ["ChargeApi/Cobros.API/Charges.API.csproj", "ChargeApi/Cobros.API/"]
COPY ["ChargeApi/Cobros.Business/Charges.Business.csproj", "ChargeApi/Cobros.Business/"]
COPY ["ChargeApi/Charge.Activity.Service.Client/Charge.Activity.Service.Client.csproj", "ChargeApi/Charge.Activity.Service.Client/"]
COPY ["ChargeApi/Chargues.Repository.Service.Client/Charges.Repository.Service.Client.csproj", "ChargeApi/Chargues.Repository.Service.Client/"]
COPY ["ChargeApi/Cobros.Action/Charges.Action.csproj", "ChargeApi/Cobros.Action/"]

RUN dotnet restore "ChargeApi/Cobros.API/Charges.API.csproj"

COPY . .
WORKDIR "/src"
#RUN dotnet build "ChargeApi/Cobros.API/Charges.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ChargeApi/Cobros.API/Charges.API.csproj" -c Release -o /app

# Build runtime image
FROM base as final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Charges.API.dll"]