#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see http://aka.ms/containercompat 

FROM microsoft/dotnet:2.1-runtime-nanoserver-1803 AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY TestClient/TestClient.csproj TestClient/
COPY XFS4IoTClient/XFS4IoTClient.csproj XFS4IoTClient/
COPY XFS4IoTCore/XFS4IoTCore.csproj XFS4IoTCore/
RUN dotnet restore TestClient/TestClient.csproj
COPY . .
WORKDIR /src/TestClient
RUN dotnet build TestClient.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish TestClient.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TestClient.dll"]
