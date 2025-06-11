# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia los archivos de los proyectos y restaura dependencias
COPY Core/Core.csproj Core/
COPY DTO/DTO.csproj DTO/
COPY Presentation/Presentation.csproj Presentation/
RUN dotnet restore Presentation/Presentation.csproj

# Copia el resto del código
COPY . .

# Publica la aplicación
RUN dotnet publish Presentation/Presentation.csproj -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "Presentation.dll"]
