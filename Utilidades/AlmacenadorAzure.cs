using Azure.Storage.Blobs;

namespace rrhh_backend.Utilidades
{
    public class AlmacenadorAzure : IAlmacenadorAzure
    {
        private string conectionString;

        public AlmacenadorAzure(IConfiguration configuration, ILogger<AlmacenadorAzure> logger)
        {
            conectionString = configuration.GetConnectionString("AzureStorage");
            logger.LogInformation("Connection String: {ConnectionString}", conectionString);
        }

        public async Task<String> GuardarImagen(string contenedor, IFormFile imagen)
        {
            var cliente = new BlobContainerClient(conectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();
            cliente.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
            var extension = Path.GetExtension(imagen.FileName);
            var nombreUnico = $"{Guid.NewGuid()}{extension}";
            var blob = cliente.GetBlobClient(nombreUnico);
            await blob.UploadAsync(imagen.OpenReadStream());
            return blob.Uri.ToString();

        }
        public async Task EliminarImagen(string ruta, string contenedor)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                return;
            }
            var cliente = new BlobContainerClient(conectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();
            var archivo = Path.GetFileName(ruta);
            var blob = cliente.GetBlobClient(archivo);
            await blob.DeleteIfExistsAsync();
        }
        public async Task<string> EditarImagen(string contenedor, IFormFile imagen, string ruta)
        {
            await EliminarImagen(ruta, contenedor);
            return await GuardarImagen(contenedor, imagen);
        }
    }
}
