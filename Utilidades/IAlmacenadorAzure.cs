
namespace rrhh_backend.Utilidades
{
    public interface IAlmacenadorAzure
    {
        Task<string> EditarImagen(string contenedor, IFormFile imagen, string ruta);
        Task EliminarImagen(string ruta, string contenedor);
        Task<string> GuardarImagen(string contenedor, IFormFile imagen);
    }
}