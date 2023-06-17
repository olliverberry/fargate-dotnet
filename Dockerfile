FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY . .
RUN dotnet restore
RUN dotnet build --no-restore -c Release -o build

FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim-amd64 AS production
WORKDIR /app
COPY --from=build build .
RUN datadog/createLogPath.sh
ENTRYPOINT [ "dotnet", "Api.dll" ]