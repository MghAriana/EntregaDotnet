using System;

namespace CentroEventos.Aplicacion;

public class ReservasAltaUseCase(IRepositorioReserva repo, ReservasValidador validacion, IRepositorioID repoID)
{
    private Reserva cargarReserva()
    {
        Console.WriteLine("Ingrese id de la persona: "); int idpersona = int.TryParse(Console.ReadLine(), out int id_p) ? id_p : -1 ;
        Console.WriteLine("Ingrese id del evento: "); int idevento = int.TryParse(Console.ReadLine(),out int id_e) ? id_e : -1;
        return new Reserva( repoID.GenerarId("Reserva"), idpersona, idevento, DateTime.Now, Estado.Pendiente);
    }
    public void Ejecutar()
    {
        Reserva unareserva = cargarReserva();
        
        if (!validacion.Validar(unareserva, out string error))
        {
            throw new Exception("Error al subir la reserva: "+ error);
        }

        Console.WriteLine("Se validó la reserva, procede el alta.");
        repo.AgregarReserva(unareserva);
    }
}
