using backendSGCS.Helpers;
using backendSGCS.Models;

namespace backendSGCS.Controllers
{
    public class CargoController
    {
        static dbSGCSContext context = dbSGCSContext.getContext();
        public static Func<Cargo, IResult> createCargo = (Cargo _cargo) => {                
            context.Cargos.Add(_cargo);
            var savedCargo = context.SaveChanges();
            return savedCargo != 0 ? Results.Ok(_cargo) : Results.NotFound(MessageHelper.createMessage(false, "Error al crear cargo"));
        };
    }
}
