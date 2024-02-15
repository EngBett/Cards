FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Cards.Api/Cards.Api.csproj", "Cards.Api/"]

RUN dotnet restore "Cards.Api/Cards.Api.csproj"
COPY . .
WORKDIR "/src/Cards.Api"
RUN dotnet build "Cards.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cards.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ARG ASPNETCORE_ENV=default
ENV ASPNETCORE_ENVIRONMENT ${ASPNETCORE_ENV}
ENTRYPOINT ["dotnet", "Cards.Api.dll"]
