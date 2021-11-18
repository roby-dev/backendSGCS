using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.EntityFrameworkCore;


namespace backendSGCS.Controllers
{
    public class UsuarioController
    {
        static dbSGCSContext context = dbSGCSContext.getContext();

        public static Func<List<Usuario>> getUsers = () => context.Usuarios.Include("MiembroProyectos").ToList();
        public static Func<int, IResult> getUserById = (int id) =>
        {
            var usuario = context.Usuarios.Find(id);
            return usuario != null ? Results.Ok(usuario) : Results.NotFound(MessageHelper.createMessage(false, "No se encontró el usuario"));
        };

        public static Func<Usuario, IResult> createUser = (Usuario _usuario) =>
        {
            _usuario.Apellidos = _usuario.Apellidos.Trim().ToUpper();
            _usuario.Nombres = _usuario.Nombres.Trim().ToUpper();
            context.Usuarios.Add(_usuario);
            var savedUser = context.SaveChanges();
            return savedUser != 0 ? Results.Ok(_usuario) : Results.NotFound(MessageHelper.createMessage(false, "Error al crear usuario"));
        };

        public static Func<int, IResult> deleteUserById = (int id) =>
        {
            var user = context.Usuarios.Find(id);
            if (user == null)
            {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el usuario"));
            }
            context.Usuarios.Remove(user);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Usuario borrado exitosamente"));
        };
    }
}