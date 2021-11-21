using backendSGCS.Helpers;
using backendSGCS.Models;

namespace backendSGCS.Controllers {
    public class AuthController {

        static dbSGCSContext context = new dbSGCSContext();

        public static Func<AuthHelper, IResult> login = (AuthHelper _body) => {
            string email = _body.correo;
            string password = _body.clave;
            var user = context.Usuario.Where(x=>x.Correo == email).FirstOrDefault();
            if (user == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No existe correo electrónico"));
            }

            bool isEqual = BCrypt.Net.BCrypt.Verify(password, user.Clave);
            user.Clave = "";
            return isEqual ? Results.Ok(user) : Results.NotFound(MessageHelper.createMessage(false, "Contraseña incorrecta"));
        };        
    }
}
