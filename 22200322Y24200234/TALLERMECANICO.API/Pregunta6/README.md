# Pregunta 6: Pruebas con Postman

La colección `TallerMecanico-Pregunta6.postman_collection.json` contiene las pruebas CRUD para:

- `TipoServicioController`: GET, POST, PUT y DELETE.
- `OrdenServicioController`: GET, POST, PUT y DELETE.

## Uso

1. Ejecutar la API con `dotnet run --project TALLERMECANICO.API`.
2. Importar el archivo JSON en Postman.
3. Verificar la variable `baseUrl` (`http://localhost:5026`).
4. Para crear órdenes, configurar `vehiculoId` con un vehículo existente y `tipoServicioId` con un tipo de servicio válido.
5. Ejecutar las carpetas en orden. Los IDs creados se guardan automáticamente en las variables de colección.

Los scripts de prueba validan los códigos HTTP esperados y permiten ejecutar la colección aunque la base esté vacía o no existan los IDs relacionados.
