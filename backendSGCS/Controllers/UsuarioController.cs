using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.EntityFrameworkCore;


namespace backendSGCS.Controllers
{
    public class UsuarioController {
        static dbSGCSContext context = new dbSGCSContext();

        public static Func<List<Usuario>> getUsers = () => context.Usuario.ToList();
        public static Func<int, IResult> getUserById = (int id) => {
            var usuario = context.Usuario.Find(id);
            return usuario != null ? Results.Ok(usuario) : Results.NotFound(MessageHelper.createMessage(false, "No se encontró el usuario"));
        };

        public static Func<Usuario, IResult> createUser = (Usuario _usuario) => {
            

            _usuario.Apellidos = ToUpperFirstLetter(_usuario.Apellidos.Trim());
            _usuario.Nombres = ToUpperFirstLetter(_usuario.Nombres.Trim());

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(_usuario.Clave);
            _usuario.Clave = passwordHash;
            try {
                context.Usuario.Add(_usuario);
                var savedUser = context.SaveChanges();
                return savedUser != 0 ? Results.Ok(_usuario) : Results.NotFound(MessageHelper.createMessage(false, "Error al crear el usuario"));
            } catch (Exception) {
                return Results.StatusCode(500);
            }
        };

        public static Func<int, Usuario, Task<IResult>> updateUser = async (int id, Usuario usuario) => {
            var _usuario = context.Usuario.Find(id);
            if (_usuario == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el usuario"));
            }
            try {
                usuario.Clave = _usuario.Clave;
                _usuario.Apellidos = ToUpperFirstLetter(_usuario.Apellidos.Trim());
                _usuario.Nombres = ToUpperFirstLetter(_usuario.Nombres.Trim());
                context.Entry(_usuario).CurrentValues.SetValues(usuario);
                await context.SaveChangesAsync();
                return Results.Ok(_usuario);
            } catch (Exception e) {
                return Results.NotFound(e);
            }
        };

        public static Func<int, Usuario, Task<IResult>> changePassword = async (int id, Usuario usuario) => {
            var _usuario = context.Usuario.Find(id);
            if (_usuario == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el usuario"));
            }
            try {
                _usuario.Clave = usuario.Clave;
                usuario = _usuario;
                context.Entry(_usuario).CurrentValues.SetValues(usuario);
                await context.SaveChangesAsync();
                return Results.Ok(_usuario);
            } catch (Exception e) {
                return Results.NotFound(e);
            }
        };

        public static Func<int, IResult> deleteUser = (int id) => {
            var user = context.Usuario.Find(id);
            if (user == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el usuario"));
            }
            context.Usuario.Remove(user);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Usuario borrado exitosamente"));
        };



        public static string ToUpperFirstLetter(string source)
        {

            source = source.ToLower();

            string[] words = source.Split(' ');
            string final = "";

            foreach (string item in words)
            {

                char[] letters = item.ToCharArray();
                letters[0] = char.ToUpper(letters[0]);
                foreach (char letter in letters)
                {
                    final = final + letter;
                }
                final = final + " ";
            }

            return final;
        }
    }


}