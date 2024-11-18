using Microsoft.AspNetCore.Mvc;
using rrhh_backend.Data.DTOs;
using rrhh_backend.Data.Models;
using rrhh_backend.Services.Rrhh;
using rrhh_backend.Utilidades;

namespace rrhh_backend.Controllers.Rrhh
{
    [ApiController]
    [Route("api/rrhh")]
    public class RrhhColaboradorController : Controller
    {
        private readonly RrhhColaboradorService _rrhhColaboradorService;
        private readonly IAlmacenadorAzure almacenadorAzure;
        private readonly string contenedor = "colaboradores";
        private readonly ILogger<RrhhColaboradorController> _logger; // Agrega el logger

        public RrhhColaboradorController(RrhhColaboradorService rrhhColaboradorService,
            IAlmacenadorAzure almacenadorAzure,
            ILogger<RrhhColaboradorController> logger)
        {
            _rrhhColaboradorService = rrhhColaboradorService;
            this.almacenadorAzure = almacenadorAzure;
            _logger = logger;
        }
        [HttpGet("listar")]
        public async Task<ActionResult<List<RHColaborador>>> GetColaboradores()
        {
            try
            {
                var colaboradores = await _rrhhColaboradorService.GetColaboradores();
                return Ok(colaboradores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener todos los colaboradores: {ex.Message}");
            }
        }
        [HttpGet("listar/sinUsuario")]
        public async Task<ActionResult<List<RHColaborador>>> GetColaboradoresSinUsuario()
        {
            try
            {
                var colaboradores = await _rrhhColaboradorService.GetColaboradoresSinUsuario();
                return Ok(colaboradores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener todos los colaboradores: {ex.Message}");
            }
        }

        [HttpPut("actualizar/{idColaborador}")]
        public async Task<ActionResult> ActualizarColaborador(int idColaborador, [FromForm] InsertColaboradorDto updateDto, [FromForm] IFormFile? foto)
        {
            try
            {
                string rutaImagen = null;
                //if (foto != null && foto.Length > 0 && foto.FileName != "viejaFoto")
                //{
                //    // Guarda la nueva imagen en el sistema de archivos
                //    var nombreUnico = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
                //    var rutaCarpetaImagenes = Path.Combine("Imagenes");
                //    rutaImagen = Path.Combine(rutaCarpetaImagenes, nombreUnico);

                //    // Guarda el nuevo logo en el sistema de archivos
                //    using (var stream = new FileStream(rutaImagen, FileMode.Create))
                //    {
                //        await foto.CopyToAsync(stream);
                //    }
                //}
                await _rrhhColaboradorService.ActualizarColaborador(idColaborador, 
                    updateDto.IdDepartamento,
                    updateDto.IdEstadoColaborador,
                    updateDto.IdEstadoCivil,
                    updateDto.Codigo,
                    updateDto.Dpi,
                    updateDto.Nombres,
                    updateDto.PrimerApellido,
                    updateDto.SegundoApellido,
                    updateDto.ApellidoCasada,
                    updateDto.MunicipioExtendido,
                    updateDto.DepartamentoExtendido,
                    updateDto.LugarNacimiento,
                    updateDto.Nacionalidad,
                    updateDto.NoIGSS,
                    updateDto.NoNIT,
                    updateDto.NombreConyuge,
                    updateDto.NoCuentaBancaria,
                    updateDto.Telefono, 
                    updateDto.Direccion, 
                    updateDto.Email,
                    updateDto.FechaNacimiento,
                    updateDto.FechaInicioLabores,
                    rutaImagen);
                return Ok(new { Message = "Colaborador actualizado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el colaborador: {ex.Message}");

            }
        }
        [HttpPost("insertar")]
        public async Task<IActionResult> InsertarColaborador([FromForm] InsertColaboradorDto insertDto, [FromForm] IFormFile? foto)
        {
            try
            {
                string rutaImagen = "n";
                //Guardar imagen local
                //if (foto != null && foto.Length > 0)
                //{
                //    var nombreUnico = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
                //    var rutaCompletaImagen = Path.Combine("Imagenes");
                //    rutaImagen = Path.Combine(rutaCompletaImagen, nombreUnico);

                //    using (var stream = new FileStream(rutaImagen, FileMode.Create))
                //    {

                //        await foto.CopyToAsync(stream);
                //    }
                //}
                //Guardar imagen en Azure
                //if (foto != null && foto.Length > 0)
                //{
                //    rutaImagen = await almacenadorAzure.GuardarImagen(contenedor, foto);
                //}
                await _rrhhColaboradorService.InsertarColaborador(
                    insertDto.IdDepartamento,
                    insertDto.IdEstadoColaborador,
                    insertDto.IdEstadoCivil,
                    insertDto.Codigo, 
                    insertDto.Dpi,
                    insertDto.Nombres, 
                    insertDto.PrimerApellido,
                    insertDto.SegundoApellido,
                    insertDto.ApellidoCasada,
                    insertDto.MunicipioExtendido,
                    insertDto.DepartamentoExtendido,
                    insertDto.LugarNacimiento,
                    insertDto.Nacionalidad,
                    insertDto.NoIGSS,
                    insertDto.NoNIT,
                    insertDto.NombreConyuge,
                    insertDto.NoCuentaBancaria,
                    insertDto.Telefono,
                    insertDto.Direccion, 
                    insertDto.Email,
                    insertDto.FechaNacimiento,
                    insertDto.FechaInicioLabores,
                    rutaImagen
                    );
                return Ok(new { Message = "Colaborador creado correctamente." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al insertar colaborador: {Message}", ex.Message);
                return StatusCode(500, $"Error al insertar el colaborador: {ex.Message}");
            }
        }

    }
}
