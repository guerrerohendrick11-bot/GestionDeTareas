# 1. Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiamos los archivos de proyecto (.csproj) respetando las carpetas
COPY ["gestionDeTareas/gestionDeTareas.csproj", "gestionDeTareas/"]
COPY ["GestionDeTareasBlazor/GestionDeTareasBlazor.csproj", "GestionDeTareasBlazor/"]

# Restauramos las dependencias (esto ahora funcionará porque ve ambos proyectos)
RUN dotnet restore "gestionDeTareas/gestionDeTareas.csproj"

# Copiamos absolutamente todo el código fuente
COPY . .

# Compilamos y publicamos el Backend
WORKDIR "/src/gestionDeTareas"
RUN dotnet publish "gestionDeTareas.csproj" -c Release -o /app/publish

# 2. Imagen final para correr la app
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Traemos lo publicado
COPY --from=build /app/publish .

# Copiamos los archivos estáticos (CSS, imágenes) del Frontend
# IMPORTANTE: Verifica que la carpeta se llame exactamente "GestionDeTareasBlazor"
COPY --from=build /src/GestionDeTareasBlazor/wwwroot ./wwwroot

ENV PORT=8080
EXPOSE 8080

# El nombre de tu DLL según vimos antes
ENTRYPOINT ["dotnet", "gestionDeTareas.dll"]
