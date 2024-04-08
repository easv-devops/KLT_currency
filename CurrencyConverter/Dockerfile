FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CurrencyConverter/CurrencyConverter.csproj", "CurrencyConverter/"]
RUN dotnet restore "CurrencyConverter/CurrencyConverter.csproj"
COPY . .
WORKDIR "/src/CurrencyConverter"
RUN dotnet build "CurrencyConverter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CurrencyConverter.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CurrencyConverter.dll"]
