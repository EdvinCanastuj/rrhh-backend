using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rrhh_backend.Data;
using rrhh_backend.Data.DTOs;
using rrhh_backend.Data.Models;

namespace rrhh_backend.Services.Rrhh
{
    public class RrhhColaboradorService
    {
        private readonly RrHhContext _context;

        public RrhhColaboradorService( RrHhContext context)
        {
            _context = context;
        }
        public async Task<List<RHListarColaboradores>> GetColaboradores()
        {
            var baseUrl = "http://localhost:5242";
            try
            {
                var colaboradores = await _context.RHColaboradors
                    .Include(l => l.RHDepartamento)
                    .Include(l => l.RHEstadoColaborador)
                    .Select(c => new RHListarColaboradores
                    {
                        IdColaborador = c.IdColaborador,
                        IdDepartamento = c.IdDepartamento,
                        NombreDepartamento = c.RHDepartamento.NombreDepartamento,
                        EstadoColaborador = c.RHEstadoColaborador.EstadosColaborador,
                        IdEstadoCivil = c.IdEstadoCivil,
                        IdEstadoColaborador = c.IdEstadoColaborador,
                        Codigo = c.Codigo,
                        Dpi = c.Dpi,
                        Nombres = c.Nombres,
                        PrimerApellido = c.PrimerApellido,
                        SegundoApellido = c.SegundoApellido,
                        ApellidoCasada = c.ApellidoCasada,
                        MunicipioExtendido = c.MunicipioExtendido,
                        DepartamentoExtendido = c.DepartamentoExtendido,
                        LugarNacimiento = c.LugarNacimiento,
                        Nacionalidad = c.Nacionalidad,
                        NoIGSS = c.NoIGSS,
                        NoNIT = c.NoNIT,
                        NombreConyuge = c.NombreConyuge,
                        NoCuentaBancaria = c.NoCuentaBancaria,
                        Telefono = c.Telefono,
                        Direccion = c.Direccion,
                        FechaNacimiento = c.FechaNacimiento,
                        FechaInicioLabores = c.FechaInicioLabores,
                        Email = c.Email,
                        Debaja = c.Debaja,
                        //Foto = string.IsNullOrEmpty(c.Foto) ? null : c.Foto.StartsWith("https://rhopgapi.blob.core.windows.net/colaboradores/") ? c.Foto : $"{baseUrl}/{c.Foto.Replace("Imagenes", "imagenes")}",
                        Foto = "",
                      })
                    .Where(c => c.Debaja == null)
                    .ToListAsync();
                return colaboradores;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las empresas: {ex.Message}");
                throw;
            }
        }public async Task<List<RHListarColaboradores>> GetColaboradoresArchivados()
        {
            var baseUrl = "http://localhost:5242";
            try
            {
                var colaboradores = await _context.RHColaboradors
                    .Include(l => l.RHDepartamento)
                    .Include(l => l.RHEstadoColaborador)
                    .Select(c => new RHListarColaboradores
                    {
                        IdColaborador = c.IdColaborador,
                        IdDepartamento = c.IdDepartamento,
                        NombreDepartamento = c.RHDepartamento.NombreDepartamento,
                        IdEstadoCivil = c.IdEstadoCivil,
                        IdEstadoColaborador = c.IdEstadoColaborador,
                        EstadoColaborador = c.RHEstadoColaborador.EstadosColaborador,
                        Codigo = c.Codigo,
                        Dpi = c.Dpi,
                        Nombres = c.Nombres,
                        PrimerApellido = c.PrimerApellido,
                        SegundoApellido = c.SegundoApellido,
                        ApellidoCasada = c.ApellidoCasada,
                        MunicipioExtendido = c.MunicipioExtendido,
                        DepartamentoExtendido = c.DepartamentoExtendido,
                        LugarNacimiento = c.LugarNacimiento,
                        Nacionalidad = c.Nacionalidad,
                        NoIGSS = c.NoIGSS,
                        NoNIT = c.NoNIT,
                        NombreConyuge = c.NombreConyuge,
                        NoCuentaBancaria = c.NoCuentaBancaria,
                        Telefono = c.Telefono,
                        Direccion = c.Direccion,
                        FechaNacimiento = c.FechaNacimiento,
                        FechaInicioLabores = c.FechaInicioLabores,
                        Email = c.Email,
                        Debaja = c.Debaja,
                        //Foto = string.IsNullOrEmpty(c.Foto) ? null : c.Foto.StartsWith("https://rhopgapi.blob.core.windows.net/colaboradores/") ? c.Foto : $"{baseUrl}/{c.Foto.Replace("Imagenes", "imagenes")}",
                        Foto = "",
                      })
                    .Where(c => c.Debaja != null)
                    .ToListAsync();
                return colaboradores;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las empresas: {ex.Message}");
                throw;
            }
        }
        public async Task<List<RHColaborador>> GetColaboradoresSinUsuario()
        {
            return await _context.RHColaboradors
                .Where(c => c.UserUser == null)
                .ToListAsync(); 
        }
        public async Task ActualizarColaborador(int idColaborador,
            int idDepartamento, 
            int idEstado,
            int idEstadoCivil,
            string? codigo, 
            string dpi,
            string nombres,
            string primerApellido,
            string? segundoApellido,
            string? apellidoCasada,
            string? municipioExtendido,
            string? departamentoExtendido,
            string? lugarNacimiento,
            string? nacionalidad,
            string? noIGSS,
            string? noNIT,
            string? nombreConyuge,
            string? noCuentaBancaria,
            string telefono, 
            string? direccion, 
            string email, 
            DateTime fechaNacimiento, 
            DateTime? fechaInicioLabores, 
            string rutaImagen)
        {
            try
            {

                var colaboradorExiste = await _context.RHColaboradors.FindAsync(idColaborador);
                if (colaboradorExiste != null && rutaImagen!= null)
                {
                    if (colaboradorExiste.IdDepartamento != idDepartamento)
                    {
                        var historial = await _context.RHHistorialDepartamentos
                            .Where(h => h.IdColaborador == idColaborador && h.FechaFin == null)
                            .FirstOrDefaultAsync();

                        if (historial != null)
                        {
                            historial.FechaFin = DateTime.Now;
                            await _context.SaveChangesAsync();
                        }

                        var nuevoHistorial = new RHHistorialDepartamento
                        {
                            IdColaborador = idColaborador,
                            IdDepartamento = idDepartamento,
                            FechaInicio = DateTime.Now,
                            FechaFin = null
                        };
                        _context.RHHistorialDepartamentos.Add(nuevoHistorial);
                    }
                    colaboradorExiste.IdDepartamento = idDepartamento;
                    colaboradorExiste.IdEstadoColaborador = idEstado;
                    colaboradorExiste.IdEstadoCivil = idEstadoCivil;
                    colaboradorExiste.Codigo = codigo;
                    colaboradorExiste.Dpi = dpi;
                    colaboradorExiste.Nombres = nombres;
                    colaboradorExiste.PrimerApellido = primerApellido;
                    colaboradorExiste.SegundoApellido = segundoApellido;
                    colaboradorExiste.ApellidoCasada = apellidoCasada;
                    colaboradorExiste.MunicipioExtendido = municipioExtendido;
                    colaboradorExiste.DepartamentoExtendido = departamentoExtendido;
                    colaboradorExiste.LugarNacimiento = lugarNacimiento;
                    colaboradorExiste.Nacionalidad = nacionalidad;
                    colaboradorExiste.NoIGSS = noIGSS;
                    colaboradorExiste.NoNIT = noNIT;
                    colaboradorExiste.NombreConyuge = nombreConyuge;
                    colaboradorExiste.NoCuentaBancaria = noCuentaBancaria;
                    colaboradorExiste.Telefono = telefono;
                    colaboradorExiste.Direccion = direccion;
                    colaboradorExiste.Email = email;
                    colaboradorExiste.FechaNacimiento = fechaNacimiento;
                    colaboradorExiste.FechaInicioLabores = fechaInicioLabores;

                    colaboradorExiste.Foto = rutaImagen;

                    await _context.SaveChangesAsync();
                }
                else if (colaboradorExiste != null && rutaImagen == null)
                {
                    if (colaboradorExiste.IdDepartamento != idDepartamento)
                    {
                        var historial = await _context.RHHistorialDepartamentos
                            .Where(h => h.IdColaborador == idColaborador && h.FechaFin == null)
                            .FirstOrDefaultAsync();

                        if (historial != null)
                        {
                            historial.FechaFin = DateTime.Now;
                            await _context.SaveChangesAsync();
                        }

                        var nuevoHistorial = new RHHistorialDepartamento
                        {
                            IdColaborador = idColaborador,
                            IdDepartamento = idDepartamento,
                            FechaInicio = DateTime.Now,
                            FechaFin = null
                        };
                        _context.RHHistorialDepartamentos.Add(nuevoHistorial);
                    }
                    colaboradorExiste.IdDepartamento = idDepartamento;
                    colaboradorExiste.IdEstadoColaborador = idEstado;
                    colaboradorExiste.IdEstadoCivil = idEstadoCivil;
                    colaboradorExiste.Codigo = codigo;
                    colaboradorExiste.Dpi = dpi;
                    colaboradorExiste.Nombres = nombres;
                    colaboradorExiste.PrimerApellido = primerApellido;
                    colaboradorExiste.SegundoApellido = segundoApellido;
                    colaboradorExiste.ApellidoCasada = apellidoCasada;
                    colaboradorExiste.MunicipioExtendido = municipioExtendido;
                    colaboradorExiste.DepartamentoExtendido = departamentoExtendido;
                    colaboradorExiste.LugarNacimiento = lugarNacimiento;
                    colaboradorExiste.Nacionalidad = nacionalidad;
                    colaboradorExiste.NoIGSS = noIGSS;
                    colaboradorExiste.NoNIT = noNIT;
                    colaboradorExiste.NombreConyuge = nombreConyuge;
                    colaboradorExiste.NoCuentaBancaria = noCuentaBancaria;
                    colaboradorExiste.Telefono = telefono;
                    colaboradorExiste.Direccion = direccion;
                    colaboradorExiste.Email = email;
                    colaboradorExiste.FechaNacimiento = fechaNacimiento;
                    colaboradorExiste.FechaInicioLabores = fechaInicioLabores;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"No se encontró el colaborador con ID: {idColaborador}");

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el colaborador: {ex.Message}");
            }
        }
        public async Task InsertarColaborador(int idDepartamento, 
            int idEstado,  
            int idEstadoCivil,
            string codigo, 
            string dpi, 
            string nombres, 
            string primerApellido,
            string? segundoApellido,
            string? apellidoCasada,
            string? municipioExtendido,
            string? departamentoExtendido,
            string? lugarNacimiento,
            string? nacionalidad,
            string? noIGSS,
            string? noNIT,
            string? nombreConyuge,
            string? noCuentaBancaria,
            string? telefono,  
            string? direccion, 
            string email, 
            DateTime fechaNacimiento, 
            DateTime? fechaInicioLabores, 
            string? rutaImagen)
        {
            try
            {
                var nuevoColaborador = new RHColaborador
                {
                    IdDepartamento = idDepartamento,
                    IdEstadoColaborador = idEstado,
                    IdEstadoCivil = idEstadoCivil,
                    Codigo = codigo,
                    Dpi = dpi,
                    Nombres = nombres,
                    PrimerApellido = primerApellido,
                    SegundoApellido = segundoApellido,
                    ApellidoCasada = apellidoCasada,
                    MunicipioExtendido = municipioExtendido,
                    DepartamentoExtendido = departamentoExtendido,
                    LugarNacimiento = lugarNacimiento,
                    Nacionalidad = nacionalidad,
                    NoIGSS = noIGSS,
                    NoNIT = noNIT,
                    NombreConyuge = nombreConyuge,
                    NoCuentaBancaria = noCuentaBancaria,
                    Telefono = telefono,
                    Direccion = direccion,
                    Email = email,
                    FechaNacimiento = fechaNacimiento,
                    FechaInicioLabores = fechaInicioLabores,
                    Debaja = null,
                    Foto = rutaImagen
                };
                _context.RHColaboradors.Add(nuevoColaborador);
                await _context.SaveChangesAsync();
                var nuevoHistorial = new RHHistorialDepartamento
                {
                    IdColaborador = nuevoColaborador.IdColaborador,
                    IdDepartamento = idDepartamento,
                    FechaInicio = DateTime.Now,
                    FechaFin = null
                };
                _context.RHHistorialDepartamentos.Add(nuevoHistorial);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar el colaborador: {ex.Message}");
            }
        }
    }
}
