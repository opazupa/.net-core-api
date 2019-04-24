FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Tests/UnitTests/UnitTests.csproj Tests/UnitTests/
COPY FeatureLibrary/FeatureLibrary.csproj FeatureLibrary/
COPY CoreLibrary/CoreLibrary.csproj CoreLibrary/
RUN dotnet restore Tests/UnitTests/UnitTests.csproj
COPY . .
WORKDIR /src/Tests/UnitTests
RUN dotnet build UnitTests.csproj -c Release -o /app

RUN dotnet test --logger:trx

ENTRYPOINT ["dotnet", "test", "--logger:trx"]