FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["IOT-mock/IOT-mock.csproj", "IOT-mock/"]
RUN dotnet restore "IOT-mock/IOT-mock.csproj"
COPY . .
WORKDIR "/src/IOT-mock"
RUN dotnet build "IOT-mock.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IOT-mock.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IOT-mock.dll"]
