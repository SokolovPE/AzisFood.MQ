FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AzisFood.MQ.Playground/AzisFood.MQ.Playground.csproj", "AzisFood.MQ.Playground/"]
RUN dotnet restore "AzisFood.MQ.Playground/AzisFood.MQ.Playground.csproj"
COPY . .
WORKDIR "/src/AzisFood.MQ.Playground"
RUN dotnet build "AzisFood.MQ.Playground.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AzisFood.MQ.Playground.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AzisFood.MQ.Playground.dll"]
