# 1. ETAPA DE COMPILACIÓN DEL FRONTEND (Blazor)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-frontend
WORKDIR /src
# Copiamos el archivo de proyecto del Frontend
COPY ["GestionDeTareasBlazor/GestionDeTareasBlazor.csproj", "GestionDeTareasBlazor/"]
RUN dotnet restore "GestionDeTareasBlazor/GestionDeTareasBlazor.csproj"
# Copiamos el resto de archivos y publicamos
COPY GestionDeTareasBlazor/ GestionDeTareasBlazor/
RUN dotnet publish "GestionDeTareasBlazor/GestionDeTareasBlazor.csproj" -c Release -o /app/frontend

# 2. ETAPA DE COMPILACIÓN DEL BACKEND (API)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-backend
WORKDIR /src
# Copiamos el archivo de proyecto del Backend
COPY ["gestionDeTareas/gestionDeTareas.csproj", "gestionDeTareas/"]
RUN dotnet restore "gestionDeTareas/gestionDeTareas.csproj"
# Copiamos el resto de archivos y publicamos
COPY gestionDeTareas/ gestionDeTareas/
RUN dotnet publish "gestionDeTareas/gestionDeTareas.csproj" -c Release -o /app/backend

# 3. IMAGEN FINAL (La que corre en Render)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Copiamos los archivos ejecutables del Backend
COPY --from=build-backend /app/backend .

# COPIAMOS EL FRONTEND DENTRO DEL BACKEND
# Esto hace que la API pueda "servir" tu página de Blazor
COPY --from=build-frontend /app/frontend/wwwroot ./wwwroot

# Configuración de puerto para Render
ENV PORT=8080
EXPOSE 8080

# Ejecutamos la DLL del Backend
ENTRYPOINT ["dotnet", "gestionDeTareas.dll"]
