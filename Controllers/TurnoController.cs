using ClassTurno.Models;
using ClassTurno.Servicio;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webAppiTurno.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        private readonly ITurnoService _turnoService;
        public TurnoController(ITurnoService turnoService)
        {
            _turnoService = turnoService;
        }
        // GET: api/<TurnoController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var turnos = await _turnoService.GetAll();
                return Ok(turnos);
            }
            catch (Exception)
            {

                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }

        // GET api/<TurnoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var turno = await _turnoService.Get(id);
                if (turno == null)
                    return NotFound($"Turno con ID {id} no encontrado");
                return Ok(turno);
            }
            catch (Exception)
            {

                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }
        [HttpGet("Servicio")]
        public async Task<IActionResult> GetAllSer()
        {
            try
            {
                var ser = await _turnoService.GetAllServicio();
                return Ok(ser);
            }
            catch (Exception)
            {

                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }
        // POST api/<TurnoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TTurno turno)
        {
            Console.WriteLine(JsonSerializer.Serialize(turno));

            if (turno == null)
                return BadRequest("El turno no puede ser nulo");
            //conmtrolar que se hayan ingresado datosde

            try
            {
                bool result = await _turnoService.Save(turno);
                if (result)
                    return Ok("Turno agregado");
                else
                    return BadRequest("Error al agregar el turno.Verifique los datos de entrada");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return StatusCode(500, $"Ha ocurrido un error interno: {ex.Message}");
            }
        }
        [HttpPost("Servicio")]
        public async Task<IActionResult> PostServicio([FromBody] TServicio servicio)
        {
            try
            {
                bool result = await _turnoService.SaveServicio(servicio);
                if (result)
                    return Ok("Servicio agregado");
                else
                    return BadRequest("Error al agregar el servicio. Verifique los datos de entrada");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Ha ocurrido un error interno: {ex.Message}"); ;
            }
        }
        // PUT api/<TurnoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TTurno turno)
        {
            if (turno == null)
                return BadRequest("El turno no puede ser nulo");
            try
            {
                bool result = await _turnoService.Update(turno, id);
                if (result)
                    return Ok("Turno actualizado");
                else
                    return NotFound("Turno no encontrado o error al actualizar");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Ha ocurrido un error interno: {ex.Message}");
            }
        }

        // DELETE api/<TurnoController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = await _turnoService.Delete(id);
                if (result)
                    return Ok("Turno eliminado");
                else
                    return NotFound("Turno no encontrado");
            }
            catch (Exception)
            {

                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }
    }
}
