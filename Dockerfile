# 1. ETAPA DE CONSTRUCCIÓN DEL FRONTEND
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-frontend
WORKDIR /src
# COINCIDENCIA DE CARPETAS: Verifica que se llame exactamente "GestionDeTareasBlazor"
COPY ["GestionDeTareasBlazor/GestionDeTareasBlazor.csproj", "GestionDeTareasBlazor/"]
RUN dotnet restore "GestionDeTareasBlazor/GestionDeTareasBlazor.csproj"
COPY GestionDeTareasBlazor/ GestionDeTareasBlazor/
# Publicamos el frontend
RUN dotnet publish "GestionDeTareasBlazor/GestionDeTareasBlazor.csproj" -c Release -o /app/frontend

# 2. ETAPA DE CONSTRUCCIÓN DEL BACKEND
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-backend
WORKDIR /src
# COINCIDENCIA DE CARPETAS: Verifica que se llame "gestionDeTareas"
COPY ["gestionDeTareas/gestionDeTareas.csproj", "gestionDeTareas/"]
RUN dotnet restore "gestionDeTareas/gestionDeTareas.csproj"
COPY gestionDeTareas/ gestionDeTareas/
RUN dotnet publish "gestionDeTareas/gestionDeTareas.csproj" -c Release -o /app/backend

# 3. IMAGEN FINAL (La que tienes tú)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Copiamos el ejecutable del Backend
COPY --from=build-backend /app/backend .

# Copiamos el contenido de wwwroot del frontend al wwwroot del backend
COPY --from=build-frontend /app/frontend/wwwroot ./wwwroot

# Configuración de puerto para Render
ENV PORT=8080
EXPOSE 8080

# IMPORTANTE: El nombre debe ser exacto al de tu proyecto .csproj
ENTRYPOINT ["dotnet", "gestionDeTareas.dll"]
