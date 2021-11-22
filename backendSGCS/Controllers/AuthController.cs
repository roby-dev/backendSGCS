using backendSGCS.Helpers;
using backendSGCS.Models;

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
    }
}