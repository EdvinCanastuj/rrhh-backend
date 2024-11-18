using Microsoft.EntityFrameworkCore;
using rrhh_backend.Data;
using rrhh_backend.Data.DTOs;
using rrhh_backend.Data.Models;

namespace rrhh_backend.Services.Rrhh
{
    public class RrhhHistorialColaboradorService
    {
        private readonly RrHhContext _context;
        public RrhhHistorialColaboradorService(RrHhContext context)
        {
            _context = context;
        }
        public async Task<List<RHListarHistorialDepartamentoDto>> GetHistorialDepartamento()
        {
            var historialDepartamento = await _context.RHHistorialDepartamentos
                .Include(hd => hd.RHColaborador)
                .Include(hd => hd.RHDepartamento)
                .Select(hd => new RHListarHistorialDepartamentoDto
                {
                    IdHistorialDepartamento = hd.IdHistorialDepartamento,
                    FechaInicio = hd.FechaInicio,
                    FechaFin = hd.FechaFin ?? DateTime.Now,
                    Departamento = hd.RHDepartamento.NombreDepartamento,
                    Nombres = hd.RHColaborador.Nombres,
                    PrimerApellido = hd.RHColaborador.PrimerApellido,
                    SegundoApellido = string.IsNullOrEmpty(hd.RHColaborador.SegundoApellido) ? string.Empty : hd.RHColaborador.SegundoApellido,
                    ApellidoCasada = string.IsNullOrEmpty(hd.RHColaborador.ApellidoCasada) ? string.Empty : hd.RHColaborador.ApellidoCasada,
                    NombreCompleto = hd.RHColaborador.Nombres + " " +
                                     hd.RHColaborador.PrimerApellido +
                                     (string.IsNullOrEmpty(hd.RHColaborador.SegundoApellido) ? "" : " " + hd.RHColaborador.SegundoApellido) +
                                     (string.IsNullOrEmpty(hd.RHColaborador.ApellidoCasada) ? "" : " " + hd.RHColaborador.ApellidoCasada)
                })
                .ToListAsync();

            return historialDepartamento;
        }
        public async Task<List<RHListarHistorialDepartamentoDto>> GetHistorialDepartamentoID(int idColaborador)
        {
            var historialDepartamento = await _context.RHHistorialDepartamentos
                .Include(hd => hd.RHColaborador)
                .Include(hd => hd.RHDepartamento)
                .Where(hd => hd.RHColaborador.IdColaborador == idColaborador)
                .Select(hd => new RHListarHistorialDepartamentoDto
                {
                    IdHistorialDepartamento = hd.IdHistorialDepartamento,
                    FechaInicio = hd.FechaInicio,
                    FechaFin = hd.FechaFin ?? DateTime.Now, //si esta nulo manda la fecha actual 
                    Departamento = hd.RHDepartamento.NombreDepartamento,
                    Nombres = hd.RHColaborador.Nombres,
                    PrimerApellido = hd.RHColaborador.PrimerApellido,
                    SegundoApellido = string.IsNullOrEmpty(hd.RHColaborador.SegundoApellido) ? string.Empty : hd.RHColaborador.SegundoApellido,
                    ApellidoCasada = string.IsNullOrEmpty(hd.RHColaborador.ApellidoCasada) ? string.Empty : hd.RHColaborador.ApellidoCasada,
                    NombreCompleto = hd.RHColaborador.Nombres + " " +
                                     hd.RHColaborador.PrimerApellido +
                                     (string.IsNullOrEmpty(hd.RHColaborador.SegundoApellido) ? "" : " " + hd.RHColaborador.SegundoApellido) +
                                     (string.IsNullOrEmpty(hd.RHColaborador.ApellidoCasada) ? "" : " " + hd.RHColaborador.ApellidoCasada)
                })
                .ToListAsync();

            return historialDepartamento;
        }



    }
}
