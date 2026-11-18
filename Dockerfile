FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["PropBuild.sln", "./"]
COPY ["src/PropBuild.Domain/PropBuild.Domain.csproj", "src/PropBuild.Domain/"]
COPY ["src/PropBuild.Application/PropBuild.Application.csproj", "src/PropBuild.Application/"]
COPY ["src/PropBuild.Infrastructure/PropBuild.Infrastructure.csproj", "src/PropBuild.Infrastructure/"]
COPY ["src/PropBuild.Api/PropBuild.Api.csproj", "src/PropBuild.Api/"]
RUN dotnet restore "src/PropBuild.Api/PropBuild.Api.csproj"
COPY . .
RUN dotnet publish "src/PropBuild.Api/PropBuild.Api.csproj" --configuration Release --output /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PropBuild.Api.dll"]
