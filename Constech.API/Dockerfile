FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Constech.API/Constech.API.csproj", "Constech.API/"]
RUN dotnet restore "Constech.API/Constech.API.csproj"
COPY . .
WORKDIR "/src/Constech.API"
RUN dotnet build "Constech.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Constech.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Constech.API.dll"]
