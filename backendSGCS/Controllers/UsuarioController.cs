using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendSGCS.Controllers {
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase {
        private readonly dbSGCSContext _context;

        public UsuarioController(dbSGCSContext context) {
            _context = context;
        }

        // GET: api/usuarios
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario() {
            return await _context.Usuario.ToListAsync();
        }

        // GET: api/usuarios/5
        [HttpGet("{id:int:required}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id) {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario is null) {
                return NotFound(MessageHelper.createMessage(false, "No se encontró el usuario"));
            }
            return usuario;
        }

        // PUT: api/usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int:required}")]
        public async Task<ActionResult<Usuario>> PutUsuario(int id, Usuario usuario) {
            var _usuario = await _context.Usuario.FindAsync(id);
            if (id != usuario.IdUsuario || _usuario is null) {
                return BadRequest(MessageHelper.createMessage(false, "Error al intentar actualizar usuario."));
            }            
            usuario.Clave = _usuario.Clave;
            usuario.Apellidos = ToUpperFirstLetter(usuario.Apellidos.Trim());
            usuario.Nombres = ToUpperFirstLetter(usuario.Nombres.Trim());
            _context.Entry(_usuario).CurrentValues.SetValues(usuario);
            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!UsuarioExists(id)) {
                    return NotFound(MessageHelper.createMessage(false, "No se encontró usuario."));
                } else {
                    return BadRequest(MessageHelper.createMessage(false, "Error al intentar actualizar usuario."));
                }
            }
            return usuario;
        }
  
        // POST: api/usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario) {
            usuario.Apellidos = ToUpperFirstLetter(usuario.Apellidos.Trim());
            usuario.Nombres = ToUpperFirstLetter(usuario.Nombres.Trim());
            usuario.Clave = BCrypt.Net.BCrypt.HashPassword(usuario.Clave);
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        // DELETE: api/usuarios/5
        [HttpDelete("{id:int:required}")]
        public async Task<IActionResult> DeleteUsuario(int id) {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario is null) {
                return NotFound(MessageHelper.createMessage(false, "No se encontró usuario."));
            }
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return Ok(MessageHelper.createMessage(true, "Usuario borrado correctamente"));
        }

        private bool UsuarioExists(int id) {
            return _context.Usuario.Any(e => e.IdUsuario == id);
        }

        private static Func<string, string> ToUpperFirstLetter = (string source) => {
            source = source.ToLower();
            string[] words = source.Split(' ');
            string final = "";
            foreach (string item in words) {
                char[] letters = item.ToCharArray();
                letters[0] = char.ToUpper(letters[0]);
                foreach (char letter in letters) {
                    final = final + letter;
                }
                final = final + " ";
            }
            return final.Trim();
        };
    }
}
