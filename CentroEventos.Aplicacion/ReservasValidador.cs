using System;

namespace CentroEventos.Aplicacion;

public class ReservasValidador(IRepositorioReserva repoReserva, IRepositorioPersona Ipersona)
{
    public bool Validar(Reserva unareserva, out string mensajeError){

        mensajeError = "";
        Console.WriteLine("Validando..");
        if (!Ipersona.existeId(unareserva.Idpersona))
        {
            mensajeError = "El dni no existe.\n";
        }
        Console.WriteLine("ok 1");
        if (!repoReserva.ExisteResposable(unareserva.Idpersona))
        {
            mensajeError = "La persona no reservo. \n";
        }
        Console.WriteLine("ok 2");
        if (repoReserva.ExisteResposable(unareserva.Idpersona) && repoReserva.existenReservas(unareserva.IdEven_Dep))
        {
            mensajeError = "Ya existe reserva para este evento /n";
        }
        Console.WriteLine("ok 3");
        if (!repoReserva.ExisteCupo(unareserva.Id))
        {
            mensajeError = "No hay cupo en este evento";
        }
        Console.WriteLine("ok 4");
        return (mensajeError == "");


    }

}
