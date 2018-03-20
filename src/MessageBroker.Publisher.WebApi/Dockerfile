FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY MessageBroker.sln ./
COPY src/MessageBroker.Publisher.WebApi/MessageBroker.Publisher.WebApi.csproj src/MessageBroker.Publisher.WebApi/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/src/MessageBroker.Publisher.WebApi
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MessageBroker.Publisher.WebApi.dll"]
