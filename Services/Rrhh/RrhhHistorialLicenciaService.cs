using Microsoft.EntityFrameworkCore;
using rrhh_backend.Data;
using rrhh_backend.Data.DTOs;

namespace rrhh_backend.Services.Rrhh
{
    public class RrhhHistorialLicenciaService
    {
        private readonly RrHhContext _context;

        public RrhhHistorialLicenciaService(RrHhContext context)
        {
            _context = context;
        }
        public async Task<List<RHListarHistorialLicenciaDto>> GetHistorialLicencia()
        {
            var historialLicencia = await _context.RHLicencias
                .Include(hl => hl.RHColaborador)
                .Include(hl => hl.RHTipoLicencias)
                .Include(hl => hl.RHEstadoLicencias)
                .Include(hl => hl.RHColaborador.RHDepartamento)
                .Select(hl => new RHListarHistorialLicenciaDto
                {
                    IdLicencia = hl.IdLicencias,
                    Nombres = hl.RHColaborador.Nombres,
                    PrimerApellido = hl.RHColaborador.PrimerApellido,
                    SegundoApellido = string.IsNullOrEmpty(hl.RHColaborador.SegundoApellido) ? "" : " " + hl.RHColaborador.SegundoApellido,
                    ApellidoCasada = string.IsNullOrEmpty(hl.RHColaborador.ApellidoCasada) ? "" : " " + hl.RHColaborador.ApellidoCasada,
                    TipoLicencia = hl.RHTipoLicencias.TipoLicencia,
                    FechaInicio = hl.FechaInicio,
                    FechaFin = hl.FechaFin,
                    Observaciones = hl.Observaciones,
                    EstadoLicencia = hl.RHEstadoLicencias.EstadoLicencia,
                    Departamento = hl.RHColaborador.RHDepartamento.NombreDepartamento,
                    NombreCompleto = hl.RHColaborador.Nombres + " " +
                                     hl.RHColaborador.PrimerApellido +
                                     (string.IsNullOrEmpty(hl.RHColaborador.SegundoApellido) ? "" : " " + hl.RHColaborador.SegundoApellido) +
                                     (string.IsNullOrEmpty(hl.RHColaborador.ApellidoCasada) ? "" : " " + hl.RHColaborador.ApellidoCasada)
                })
                .ToListAsync();
            return historialLicencia;
        }
        public async Task<List<RHListarHistorialLicenciaDto>> GetHistorialLicenciaId(int IdColaborador, DateTime fechaInicio, DateTime fechaFin)
        {
            var historialLicencia = await _context.RHLicencias
                .Include(hl => hl.RHColaborador)
                .Include(hl => hl.RHTipoLicencias)
                .Include(hl => hl.RHEstadoLicencias)
                .Include(hl => hl.RHColaborador.RHDepartamento)
                .Where(hl => hl.RHColaborador.IdColaborador == IdColaborador && hl.FechaInicio >= fechaInicio && hl.FechaFin <= fechaFin)
                .Select(hl => new RHListarHistorialLicenciaDto
                {
                    IdLicencia = hl.IdLicencias,
                    Nombres = hl.RHColaborador.Nombres,
                    PrimerApellido = hl.RHColaborador.PrimerApellido,
                    SegundoApellido = string.IsNullOrEmpty(hl.RHColaborador.SegundoApellido) ? "" : " " + hl.RHColaborador.SegundoApellido,
                    ApellidoCasada = string.IsNullOrEmpty(hl.RHColaborador.ApellidoCasada) ? "" : " " + hl.RHColaborador.ApellidoCasada,
                    TipoLicencia = hl.RHTipoLicencias.TipoLicencia,
                    FechaInicio = hl.FechaInicio,
                    FechaFin = hl.FechaFin,
                    Observaciones = hl.Observaciones,
                    EstadoLicencia = hl.RHEstadoLicencias.EstadoLicencia,
                    Departamento = hl.RHColaborador.RHDepartamento.NombreDepartamento,
                    NombreCompleto = hl.RHColaborador.Nombres + " " +
                                     hl.RHColaborador.PrimerApellido +
                                     (string.IsNullOrEmpty(hl.RHColaborador.SegundoApellido) ? "" : " " + hl.RHColaborador.SegundoApellido) +
                                     (string.IsNullOrEmpty(hl.RHColaborador.ApellidoCasada) ? "" : " " + hl.RHColaborador.ApellidoCasada)
                })
                .ToListAsync();
            return historialLicencia;
        }
        public async Task<List<RHListarHistorialLicenciaDto>> GetHistorialLicenciaGeneral( DateTime fechaInicio, DateTime fechaFin)
        {
            var historialLicencia = await _context.RHLicencias
                .Include(hl => hl.RHColaborador)
                .Include(hl => hl.RHTipoLicencias)
                .Include(hl => hl.RHEstadoLicencias)
                .Include(hl => hl.RHColaborador.RHDepartamento)
                .Where(hl => hl.FechaInicio >= fechaInicio && hl.FechaFin <= fechaFin)
                .Select(hl => new RHListarHistorialLicenciaDto
                {
                    IdLicencia = hl.IdLicencias,
                    Nombres = hl.RHColaborador.Nombres,
                    PrimerApellido = hl.RHColaborador.PrimerApellido,
                    SegundoApellido = string.IsNullOrEmpty(hl.RHColaborador.SegundoApellido) ? "" : " " + hl.RHColaborador.SegundoApellido,
                    ApellidoCasada = string.IsNullOrEmpty(hl.RHColaborador.ApellidoCasada) ? "" : " " + hl.RHColaborador.ApellidoCasada,
                    TipoLicencia = hl.RHTipoLicencias.TipoLicencia,
                    FechaInicio = hl.FechaInicio,
                    FechaFin = hl.FechaFin,
                    Observaciones = hl.Observaciones,
                    EstadoLicencia = hl.RHEstadoLicencias.EstadoLicencia,
                    Departamento = hl.RHColaborador.RHDepartamento.NombreDepartamento,
                    NombreCompleto = hl.RHColaborador.Nombres + " " +
                                     hl.RHColaborador.PrimerApellido +
                                     (string.IsNullOrEmpty(hl.RHColaborador.SegundoApellido) ? "" : " " + hl.RHColaborador.SegundoApellido) +
                                     (string.IsNullOrEmpty(hl.RHColaborador.ApellidoCasada) ? "" : " " + hl.RHColaborador.ApellidoCasada)
                })
                .ToListAsync();
            return historialLicencia;
        }

    }
}
