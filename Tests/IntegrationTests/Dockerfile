FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Tests/IntegrationTests/IntegrationTests.csproj Tests/IntegrationTests/
COPY API/API.csproj API/
COPY FeatureLibrary/FeatureLibrary.csproj FeatureLibrary/
COPY CoreLibrary/CoreLibrary.csproj CoreLibrary/
RUN dotnet restore Tests/IntegrationTests/IntegrationTests.csproj
COPY . .
WORKDIR /src/Tests/IntegrationTests
RUN dotnet build IntegrationTests.csproj -c Release -o /app

RUN dotnet test --logger:trx

ENTRYPOINT ["dotnet", "test", "--logger:trx"]
