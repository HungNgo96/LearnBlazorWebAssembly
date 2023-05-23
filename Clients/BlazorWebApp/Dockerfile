FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BlazorWebApp/BlazorWebApp.csproj", "BlazorWebApp/"]
COPY ["ApplicationClient/ApplicationClient.csproj", "ApplicationClient/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "./BlazorWebApp/BlazorWebApp.csproj"
RUN dotnet restore "./ApplicationClient/ApplicationClient.csproj"
RUN dotnet restore "./Shared/Shared.csproj"
COPY ./BlazorWebApp BlazorWebApp/
COPY ./ApplicationClient ApplicationClient/
COPY ./Shared Shared/
WORKDIR "/src/."
RUN dotnet build "BlazorWebApp/BlazorWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorWebApp/BlazorWebApp.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot/ .
COPY ["BlazorWebApp/nginx.conf", "/etc/nginx/nginx.conf"]
EXPOSE 80