FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ENV ASPNETCORE_ENVIRONMENT=Development

COPY ["ProjectManagerApi.Application/ProjectManagerApi.Application.csproj", "ProjectManagerApi.Application/"]
COPY ["ProjectManagerApi.Domain/ProjectManagerApi.Domain.csproj", "ProjectManagerApi.Domain/"]
COPY ["ProjectManagerApi.Infrastructure/ProjectManagerApi.Infrastructure.csproj", "ProjectManagerApi.Infrastructure/"]
COPY ["ProjectManagerApi.Tests/ProjectManagerApi.Tests.csproj", "ProjectManagerApi.Tests/"]
COPY ["ProjectManagerApi/ProjectManagerApi.csproj", "ProjectManagerApi/"]

RUN dotnet restore "ProjectManagerApi/ProjectManagerApi.csproj"

COPY . .
WORKDIR "/src/ProjectManagerApi"
RUN dotnet build "ProjectManagerApi.csproj" -c Development -o /app/build

FROM build AS testrunner
WORKDIR /src
RUN dotnet test "ProjectManagerApi.Tests/ProjectManagerApi.Tests.csproj" --no-build --verbosity normal

FROM build AS publish
RUN dotnet publish "ProjectManagerApi.csproj" -c Development -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "ProjectManagerApi.dll"]