using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backendSGCS.Models;
using backendSGCS.Helpers;

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
            return await _context.VersionElementoConfiguracion
                                 .Include(x => x.IdSolicitudNavigation.IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdProyectoNavigation.IdMetodologiaNavigation)
                                 .Include(x => x.IdSolicitudNavigation.IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdEntregableNavigation.IdFaseMetodologiaNavigation.IdMetodologiaNavigation)
                                 .Include(x => x.IdSolicitudNavigation.IdMiembroProyectoNavigation.IdUsuarioNavigation)
                                 .Include(x => x.IdSolicitudNavigation.IdMiembroProyectoNavigation.IdProyectoNavigation)
                                 .Include(x => x.IdSolicitudNavigation.IdMiembroProyectoNavigation.IdCargoNavigation)
                                 .ToListAsync();
        }

        [HttpGet("proyectos/usuario/{id:int:required}")]
        public ActionResult<IEnumerable<VersionElementoConfiguracion>> getVersionesByProjectByUser(int id) {
            List<VersionElementoConfiguracion> versionesByProject = new List<VersionElementoConfiguracion>();
            var miembroProyecto = _context.MiembroProyecto
                                                .Include(x=>x.IdProyectoNavigation)                                                
                                                .Include(x=>x.IdCargoNavigation)
                                                .Where(x=>x.IdUsuario==id)
                                                .ToList();     

            if(miembroProyecto.Count == 0) {
                return NotFound(MessageHelper.createMessage(false, "No se encontraron proyectos para este usuario"));
            }

            miembroProyecto.ForEach(miembro => {
                var versiones = _context.VersionElementoConfiguracion
                                               .Include(x => x.IdSolicitudNavigation.IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdProyectoNavigation.IdMetodologiaNavigation)
                                               .Include(x => x.IdSolicitudNavigation.IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdEntregableNavigation.IdFaseMetodologiaNavigation.IdMetodologiaNavigation)
                                               .Include(x => x.IdSolicitudNavigation.IdMiembroProyectoNavigation.IdUsuarioNavigation)
                                               .Include(x => x.IdSolicitudNavigation.IdMiembroProyectoNavigation.IdProyectoNavigation)
                                               .Include(x => x.IdSolicitudNavigation.IdMiembroProyectoNavigation.IdCargoNavigation)
                                               .Where(x => x.IdSolicitudNavigation.IdMiembroProyectoNavigation.IdProyecto == miembro.IdProyecto)
                                               .ToList();
                if(versiones.Count != 0) {
                    versiones.ForEach(version => {
                        versionesByProject.Add(version);
                    });
                }                
            });

            versionesByProject = versionesByProject.Distinct().ToList();

            if (versionesByProject.Count == 0) {
                return NotFound(MessageHelper.createMessage(false, "No se encontraron versiones relacionadas para este usuario"));
            }

            return Ok(versionesByProject);
        }


        [HttpGet("usuario/{id:int:required}")]
        public async Task<ActionResult<IEnumerable<VersionElementoConfiguracion>>> getVersionesByUser(int id) {
            List<VersionElementoConfiguracion> versionesByUser = new List<VersionElementoConfiguracion>();
         
           
            versionesByUser = await _context.VersionElementoConfiguracion
                                            .Include(x => x.IdSolicitudNavigation.IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdProyectoNavigation.IdMetodologiaNavigation)
                                            .Include(x => x.IdSolicitudNavigation.IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdEntregableNavigation.IdFaseMetodologiaNavigation.IdMetodologiaNavigation)
                                            .Include(x => x.IdSolicitudNavigation.IdMiembroProyectoNavigation.IdUsuarioNavigation)
                                            .Include(x => x.IdSolicitudNavigation.IdMiembroProyectoNavigation.IdProyectoNavigation)
                                            .Include(x => x.IdSolicitudNavigation.IdMiembroProyectoNavigation.IdCargoNavigation)
                                            .Where(x => x.IdSolicitudNavigation.IdMiembroProyectoNavigation.IdUsuario == id)
                                            .ToListAsync();                       

            if (versionesByUser.Count == 0) {
                return NotFound(MessageHelper.createMessage(false, "No se encontraron versiones de este usuario"));
            }

            return Ok(versionesByUser);
        }

        // GET: api/VersionElementoConfiguracion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VersionElementoConfiguracion>> GetVersionElementoConfiguracion(int id)
        {
            var versionElementoConfiguracion = await _context.VersionElementoConfiguracion
                                 .Include(x => x.IdSolicitudNavigation.IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdProyectoNavigation.IdMetodologiaNavigation)
                                 .Include(x => x.IdSolicitudNavigation.IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdEntregableNavigation.IdFaseMetodologiaNavigation.IdMetodologiaNavigation)
                                 .Include(x => x.IdSolicitudNavigation.IdMiembroProyectoNavigation.IdUsuarioNavigation)
                                 .Include(x => x.IdSolicitudNavigation.IdMiembroProyectoNavigation.IdProyectoNavigation)
                                 .Include(x => x.IdSolicitudNavigation.IdMiembroProyectoNavigation.IdCargoNavigation)
                                 .Where(x=>x.IdVersion==id).FirstOrDefaultAsync();
                                                             

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

            return Ok(versionElementoConfiguracion);
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

            return Ok(MessageHelper.createMessage(true, "Version elimnada correctamente"));
        }

        private bool VersionElementoConfiguracionExists(int id)
        {
            return _context.VersionElementoConfiguracion.Any(e => e.IdVersion == id);
        }
    }
}
