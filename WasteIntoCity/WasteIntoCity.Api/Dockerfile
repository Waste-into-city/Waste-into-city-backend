# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# ARG BUILDCONFIG=Release
# ARG VERSION=1.0.0

# WORKDIR /build

# # Сначала копируем все .csproj файлы для корректной работы зависимостей
# COPY ./WasteIntoCity.Application/WasteIntoCity.Application.csproj ./WasteIntoCity.Application/
# COPY ./WasteIntoCity.Core/WasteIntoCity.Core.csproj ./WasteIntoCity.Core/
# COPY ./WasteIntoCity.Infrastructure/WasteIntoCity.Infrastructure.csproj ./WasteIntoCity.Infrastructure/
# COPY ./WasteIntoCity.Persistance/WasteIntoCity.Persistance.csproj ./WasteIntoCity.Persistance/
# COPY ./WasteIntoCity.Api/WasteIntoCity.Api.csproj ./WasteIntoCity.Api/

# # Восстанавливаем зависимости
# RUN dotnet restore ./WasteIntoCity.Api/WasteIntoCity.Api.csproj

# # Копируем весь проект
# COPY . .

# # Публикуем проект
# RUN dotnet publish ./WasteIntoCity.Api/WasteIntoCity.Api.csproj -c $BUILDCONFIG -o /app/out /p:Version=$VERSION

# FROM mcr.microsoft.com/dotnet/aspnet:8.0
# WORKDIR /app
# COPY --from=build /app/out .

# ENTRYPOINT ["dotnet", "WasteIntoCity.Api.dll"]


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Add SSL Certificate support
ENV ASPNETCORE_URLS=https://+:8081;http://+:8080
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=YourSecurePassword

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WasteIntoCity.Api/WasteIntoCity.Api.csproj", "WasteIntoCity.Api/"]
RUN dotnet restore "WasteIntoCity.Api/WasteIntoCity.Api.csproj"
COPY . .
WORKDIR "/src/WasteIntoCity.Api"
RUN dotnet build "WasteIntoCity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WasteIntoCity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WasteIntoCity.Api.dll"]

