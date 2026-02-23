# 1. Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiamos todo el código
COPY . .

# Restauramos dependencias
RUN dotnet restore "gestionDeTareas/gestionDeTareas.csproj"

# --- EL TRUCO MÁGICO: Borramos los archivos duplicados del frontend ---
# Esto evita el error NETSDK1152 de duplicados
RUN rm -f GestionDeTareasBlazor/appsettings.json GestionDeTareasBlazor/appsettings.Development.json

# Ahora publicamos el Backend sin conflictos
RUN dotnet publish "gestionDeTareas/gestionDeTareas.csproj" -c Release -o /app/publish

# 2. Imagen final
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Traemos lo publicado
COPY --from=build /app/publish .

# Copiamos los archivos estáticos (CSS, etc)
COPY --from=build /src/GestionDeTareasBlazor/wwwroot ./wwwroot

ENV PORT=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "gestionDeTareas.dll"]
