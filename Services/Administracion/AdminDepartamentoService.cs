using Microsoft.EntityFrameworkCore;
using rrhh_backend.Data;
using rrhh_backend.Data.Models;

namespace rrhh_backend.Services.Administracion
{
    public class AdminDepartamentoService
    {
        private readonly RrHhContext _context;

        public AdminDepartamentoService(RrHhContext context)
        {
            _context = context;
        }

        public async Task<List<RHDepartamento>> GetAllDepartamentos()
        {
            return await _context.RHDepartamento.ToListAsync();
        }


        //insertamos nuevos departamentos
        public async Task CrearDepartamento(string nombreDepartamento, string descripcionDepartamento)
        {
            try
            {
                var nuevoDepartamento = new RHDepartamento
                {
                    NombreDepartamento = nombreDepartamento,
                    DescripcionDepartamento = descripcionDepartamento
                };
                _context.RHDepartamento.Add(nuevoDepartamento);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear departamento: {ex.Message}");
                throw;
            }
        }

        // Buscar un módulo por su ID
        public async Task<RHDepartamento> FindDepartamentoById(int idModulo)
        {
            return await _context.RHDepartamento.FindAsync(idModulo);
        }

        // Actualizar un módulo existente
        public async Task ActualizarDepartamento(int idDepartamento, string nuevoNombre, string nuevaDescripcion)
        {
            try
            {
                var moduloExistente = await _context.RHDepartamento.FindAsync(idDepartamento);
                if (moduloExistente != null)
                {
                    moduloExistente.NombreDepartamento = nuevoNombre;
                    moduloExistente.DescripcionDepartamento = nuevaDescripcion;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine($"No se encontró el módulo con ID: {idDepartamento}");
                    // Aquí puedes manejar el caso cuando el módulo no se encuentra, por ejemplo, lanzar una excepción
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el módulo: {ex.Message}");
                throw;
            }
        }


    }

}
