# 1. Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiamos TODOS los archivos de la solución para que las referencias funcionen
COPY . .

# Restauramos las dependencias de toda la solución
RUN dotnet restore "gestionDeTareas/gestionDeTareas.csproj"

# Publicamos el Backend desde la RAÍZ (esto es vital para que vea al Frontend)
RUN dotnet publish "gestionDeTareas/gestionDeTareas.csproj" -c Release -o /app/publish

# 2. Imagen final para correr la app
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Traemos lo publicado
COPY --from=build /app/publish .

# Copiamos los archivos estáticos (CSS, imágenes)
# Asegúrate de que el nombre de la carpeta sea exacto
COPY --from=build /src/GestionDeTareasBlazor/wwwroot ./wwwroot

ENV PORT=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "gestionDeTareas.dll"]
