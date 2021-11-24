using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendSGCS.Controllers {
    [Route("api/cargos")]
    [ApiController]
    public class CargoController : ControllerBase {
        private readonly dbSGCSContext _context;

        public CargoController(dbSGCSContext context) {
            _context = context;
        }

        // GET: api/cargos
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Cargo>>> getCargos() {
            return await _context.Cargo.ToListAsync();
        }

        // GET: api/cargos/5
        [HttpGet("{id:int:required}")]
        public async Task<ActionResult<Cargo>> getCargoById(int id) {
            var cargo = await _context.Cargo.FindAsync(id);
            return cargo is null ? NotFound(MessageHelper.createMessage(false, "Cargo no encontrado.")) : cargo;
        }

        // PUT: api/Cargo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int:required}")]
        public async Task<ActionResult<Cargo>> PutCargo(int id, Cargo cargo) {
            if (id != cargo.IdCargo) {
                return BadRequest(MessageHelper.createMessage(false, "Error al intentar actualizar cargo."));
            }
            cargo.Nombre = cargo.Nombre.Trim();
            _context.Entry(cargo).State = EntityState.Modified;
            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!CargoExists(id)) {
                    return NotFound(MessageHelper.createMessage(false, "No se encontró cargo."));
                } else {
                    return BadRequest(MessageHelper.createMessage(false, "Error al intentar actualizar cargo."));
                }
            }
            return cargo;
        }

        // POST: api/Cargo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cargo>> PostCargo(Cargo cargo) {
            cargo.Nombre = cargo.Nombre.Trim();
            Cargo? isCargoExists = _context.Cargo.Where(x=>x.Nombre==cargo.Nombre).FirstOrDefault();
            if (isCargoExists == null) {
                _context.Cargo.Add(cargo);
                await _context.SaveChangesAsync();
                return cargo;
            } 
            return BadRequest(MessageHelper.createMessage(false, "Cargo ya registrado"));                       
        }

        // DELETE: api/Cargo/5
        [HttpDelete("{id:int:required}")]
        public async Task<IActionResult> DeleteCargo(int id) {
            var cargo = await _context.Cargo.FindAsync(id);
            if (cargo == null) {
                return NotFound(MessageHelper.createMessage(false,"No se encontró cargo"));
            }

            _context.Cargo.Remove(cargo);
            await _context.SaveChangesAsync();
            return Ok(MessageHelper.createMessage(true,"Cargo borrado correctamente"));
        }

        private bool CargoExists(int id) {
            return _context.Cargo.Any(e => e.IdCargo == id);
        }
    }
}
