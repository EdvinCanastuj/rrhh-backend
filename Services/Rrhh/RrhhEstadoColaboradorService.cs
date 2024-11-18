using Microsoft.EntityFrameworkCore;
using rrhh_backend.Data;
using rrhh_backend.Data.Models;

namespace rrhh_backend.Services.Rrhh
{
    public class RrhhEstadoColaboradorService
    {
        private readonly RrHhContext _context;

        public RrhhEstadoColaboradorService( RrHhContext context)
        {
            _context = context;
        }
        public async Task<List<RHEstadoColaborador>> GetEstadosColaborador()
        {
            return await _context.RHEstadoColaboradors.ToListAsync();
        }
    }
}
