using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backendSGCS.Models;

namespace backendSGCS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionElementoConfiguracionController : ControllerBase
    {
        private readonly dbSGCSContext _context;

        public VersionElementoConfiguracionController(dbSGCSContext context)
        {
            _context = context;
        }

        // GET: api/VersionElementoConfiguracion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VersionElementoConfiguracion>>> GetVersionElementoConfiguracion()
        {
            return await _context.VersionElementoConfiguracion.ToListAsync();
        }

        // GET: api/VersionElementoConfiguracion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VersionElementoConfiguracion>> GetVersionElementoConfiguracion(int id)
        {
            var versionElementoConfiguracion = await _context.VersionElementoConfiguracion.FindAsync(id);

            if (versionElementoConfiguracion == null)
            {
                return NotFound();
            }

            return versionElementoConfiguracion;
        }

        // PUT: api/VersionElementoConfiguracion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVersionElementoConfiguracion(int id, VersionElementoConfiguracion versionElementoConfiguracion)
        {
            if (id != versionElementoConfiguracion.IdVersion)
            {
                return BadRequest();
            }

            _context.Entry(versionElementoConfiguracion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VersionElementoConfiguracionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/VersionElementoConfiguracion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VersionElementoConfiguracion>> PostVersionElementoConfiguracion(VersionElementoConfiguracion versionElementoConfiguracion)
        {
            _context.VersionElementoConfiguracion.Add(versionElementoConfiguracion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVersionElementoConfiguracion", new { id = versionElementoConfiguracion.IdVersion }, versionElementoConfiguracion);
        }

        // DELETE: api/VersionElementoConfiguracion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVersionElementoConfiguracion(int id)
        {
            var versionElementoConfiguracion = await _context.VersionElementoConfiguracion.FindAsync(id);
            if (versionElementoConfiguracion == null)
            {
                return NotFound();
            }

            _context.VersionElementoConfiguracion.Remove(versionElementoConfiguracion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VersionElementoConfiguracionExists(int id)
        {
            return _context.VersionElementoConfiguracion.Any(e => e.IdVersion == id);
        }
    }
}
