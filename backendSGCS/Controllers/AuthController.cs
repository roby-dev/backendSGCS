using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.EntityFrameworkCore;

namespace backendSGCS.Controllers {
    public class AuthController {

        public static Func<AuthHelper, IResult> login = (AuthHelper _body) => {
            dbSGCSContext context = new();
            string email = _body.correo;
            string password = _body.clave;
            Usuario? user = context.Usuario.Where(x => x.Correo == email).FirstOrDefault();
            if (user != null) {
                bool isPasswordsEquals = BCrypt.Net.BCrypt.Verify(password, user.Clave);
                return !isPasswordsEquals
                    ? Results.NotFound(MessageHelper.createMessage(false, "clave incorrecta"))
                    : user.Estado ? Results.Ok(user) : Results.NotFound(MessageHelper.createMessage(false, "usuario inactivo"));
            }
            return Results.NotFound(MessageHelper.createMessage(false, "no existe el correo electrónico"));
        };

        public static Func<int, string, IResult> changePassword = (int id, string clave) => {
            dbSGCSContext _context = new();
            var _usuario = _context.Usuario.Find(id);
            if (_usuario is null) {
                return Results.BadRequest(MessageHelper.createMessage(false, "Error al intentar actualizar usuario."));
            }
            _usuario.Clave = BCrypt.Net.BCrypt.HashPassword(clave);
            _context.Entry(_usuario).State = EntityState.Modified;
            try {
                _context.SaveChanges();
            } catch (DbUpdateConcurrencyException) {
                return Results.BadRequest(MessageHelper.createMessage(false, "Error al intentar actualizar usuario."));

            }
            return Results.Ok(_usuario);
        };
    }
}