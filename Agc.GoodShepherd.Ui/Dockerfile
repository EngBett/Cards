FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Laundry.Ui/Laundry.Ui.csproj", "Laundry.Ui/"]
RUN dotnet restore "Laundry.Ui/Laundry.Ui.csproj"
COPY . .
WORKDIR "/src/Laundry.Ui"
RUN dotnet build "Laundry.Ui.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Laundry.Ui.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Laundry.Ui.dll"]
