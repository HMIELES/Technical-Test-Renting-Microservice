# Renting Microservice

Este microservicio gestiona el flujo de alquiler de vehículos, incluyendo la creación de clientes, vehículos y el proceso de rentar y devolver vehículos. Ha sido diseñado bajo principios de Clean Architecture y es completamente portable mediante Docker.

## Características principales

- Arquitectura en capas (Domain, Application, Infrastructure, WebApi).
- Contenedorización con Docker y docker-compose.
- Pruebas automáticas: unitarias, funcionales e infraestructura.
- Cobertura de pruebas básica incluida (21 tests).
- API REST con endpoints para operaciones clave.

## Tecnologías utilizadas

- .NET 7.0
- ASP.NET Core Web API
- xUnit (testing)
- Docker / Docker Compose

## Estructura de carpetas

```
├── src
│   ├── Renting.Application
│   ├── Renting.Domain
│   ├── Renting.Infrastructure
│   └── Renting.WebApi
├── tests
│   └── Renting.UnitTests
└── docker-compose.yml
```

## Endpoints principales

- `POST /api/vehicles/create`: Crear un nuevo vehículo.
- `POST /api/vehicles/rent`: Rentar un vehículo.
- `POST /api/vehicles/return`: Devolver un vehículo.
- `GET /api/vehicles/ping`: Verifica si el microservicio está vivo. Responde con 200 OK si todo está funcionando correctamente.

## Cómo ejecutar el microservicio con Docker

### Opción 1: Docker Desktop (GUI)

1. Instala [Docker Desktop](https://www.docker.com/products/docker-desktop/).
2. Clona el repositorio:

```bash
git clone https://github.com/tu-usuario/renting-microservice.git
cd renting-microservice
```

3. Ejecuta:

```bash
docker compose build
docker compose up
```

4. Accede desde tu navegador a:
[http://localhost:5000/api/vehicles/ping](http://localhost:5000/api/vehicles/ping)

### Opción 2: Docker CLI (sin interfaz gráfica)

1. Instala Docker siguiendo esta guía: https://docs.docker.com/engine/install
2. Clona el repositorio y ejecuta:

```bash
git clone https://github.com/tu-usuario/renting-microservice.git
cd renting-microservice

docker compose build
docker compose up
```

## Verificación del estado del microservicio

Una vez levantado, puedes verificar que el servicio esté activo ejecutando:

```
curl http://localhost:5000/api/vehicles/ping
```

Deberías recibir una respuesta `200 OK`, lo cual indica que el microservicio está vivo.

## Pruebas automáticas

Este microservicio incluye pruebas para garantizar calidad y facilitar la evolución del código:

- **Unitarias**: Validan lógica pura de negocio.
- **Funcionales**: Prueban el flujo de negocio completo con infraestructura simulada.
- **Infraestructura**: Validan que los endpoints respondan correctamente a nivel de host.

Las pruebas se ejecutan automáticamente al correr `docker compose build`. Se espera que todos los tests pasen correctamente (21 en total).

## Autor

Este microservicio fue desarrollado como parte de una prueba técnica. 
Cumple con todos los requisitos de contenedorización, pruebas automáticas y buenas prácticas de arquitectura.

Desarrollado por [Tu Nombre].

---

¿Dudas o sugerencias? ¡Contribuciones y mejoras son bienvenidas!
