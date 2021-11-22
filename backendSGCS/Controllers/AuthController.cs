using backendSGCS.Helpers;
using backendSGCS.Models;

namespace backendSGCS.Controllers {
    public class AuthController {

        public static Func<AuthHelper, IResult> login = (AuthHelper _body) => {
            dbSGCSContext context = new dbSGCSContext();
            string email = _body.correo;
            string password = _body.clave;
            var user = context.Usuario.Where(x => x.Correo == email).FirstOrDefault();
            if (user == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No existe el correo electrónico"));
            }
            bool isEqual = BCrypt.Net.BCrypt.Verify(password, user.Clave);
            if (isEqual) {
                return user.Estado ? Results.Ok(user) : Results.NotFound(MessageHelper.createMessage(false, "Usuario inactivo"));
            } else {
                return Results.NotFound(MessageHelper.createMessage(false, "Clave incorrecta"));
            }
        };
    }
}