using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendSGCS.Controllers {
    [Route("api/fasesMetodologia")]
    [ApiController]
    public class FaseMetodologiaController : ControllerBase {
        private readonly dbSGCSContext _context;

        public FaseMetodologiaController(dbSGCSContext context) {
            _context = context;
        }

        // GET: api/fasesMetodologia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FaseMetodologia>>> GetFaseMetodologia() {
            return await _context.FaseMetodologia.Include(x => x.IdMetodologiaNavigation).ToListAsync();
        }

        // GET: api/fasesMetodologia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FaseMetodologia>> GetFaseMetodologia(int id) {
            var faseMetodologia = await  _context.FaseMetodologia.Include(x => x.IdMetodologiaNavigation).Where(x => x.IdFaseMetodologia == id).FirstOrDefaultAsync();
            if (faseMetodologia is null) {
                return NotFound(MessageHelper.createMessage(false, "No se encontró la fase de metdología"));
            }
            return faseMetodologia;
        }

        // PUT: api/fasesMetodologia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<FaseMetodologia>> PutFaseMetodologia(int id, FaseMetodologia faseMetodologia) {
            if (id != faseMetodologia.IdFaseMetodologia) {
                return BadRequest(MessageHelper.createMessage(false, "Error al intentar actualizar al fase de metodología"));
            }
            faseMetodologia.Nombre = faseMetodologia.Nombre.Trim();
            _context.Entry(faseMetodologia).State = EntityState.Modified;
            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!FaseMetodologiaExists(id)) {
                    return NotFound(MessageHelper.createMessage(false, "No se encontró la fase de metodología"));
                } else {
                    return BadRequest(MessageHelper.createMessage(false, "Error al intentar actualizar al fase de metodología"));
                }
            }
            return faseMetodologia;
        }

        // POST: api/fasesMetodologia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FaseMetodologia>> PostFaseMetodologia(FaseMetodologia faseMetodologia) {
            try {
                faseMetodologia.Nombre = faseMetodologia.Nombre.Trim();
                _context.FaseMetodologia.Add(faseMetodologia);
                await _context.SaveChangesAsync();
                return faseMetodologia;
            } catch (Exception) {
                return BadRequest(MessageHelper.createMessage(false, "Error al intentar actualizar al fase de metodología"));
            }
        }

        // DELETE: api/fasesMetodologia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaseMetodologia(int id) {
            var faseMetodologia = await _context.FaseMetodologia.FindAsync(id);
            if (faseMetodologia is null) {
                return NotFound(MessageHelper.createMessage(false, "No se encontró la fase de metodología"));
            }
            _context.FaseMetodologia.Remove(faseMetodologia);
            await _context.SaveChangesAsync();
            return Ok(MessageHelper.createMessage(true, "Fase de metodología borrada correctamente"));
        }

        private bool FaseMetodologiaExists(int id) {
            return _context.FaseMetodologia.Any(e => e.IdFaseMetodologia == id);
        }
    }
}
