using Microsoft.EntityFrameworkCore;
using rrhh_backend.Data;
using rrhh_backend.Data.Models;

namespace rrhh_backend.Services.Rrhh
{
    public class RrhhEstadoCivilService
    {
        private readonly RrHhContext _context;

        public RrhhEstadoCivilService(RrHhContext context)
        {
            _context = context;
        }

        public async Task<List<RHEstadoCivil>> GetEstadosCiviles()
        {
            return await _context.RHEstadoCivils.ToListAsync();
        }
    }
}
