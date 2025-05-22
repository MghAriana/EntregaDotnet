using System;

namespace CentroEventos.Aplicacion;

public class ReservasValidador(IRepositorioReserva repoReserva, IRepositorioPersona repoPersona)
{
    public bool Validar(Reserva reserva, out string mensajeError){

        mensajeError = "";
        Console.WriteLine("Validando...");
        if (!repoPersona.existeId(reserva.Idpersona))
        {
            mensajeError = $"No existe persona con id {reserva.Idpersona}";
        }
        if (!repoReserva.ExisteResposable(reserva.Idpersona))
        {
            mensajeError = "La persona no reservo. \n";
        }
        if (repoReserva.existeReservaRegistrada(reserva.Idpersona,reserva.IdEven_Dep))
        {
            mensajeError = "La persona ingresada ya posee una reserva para este evento /n";
        }
        if (!repoReserva.ExisteCupo(reserva.IdEven_Dep))
        {
            mensajeError = "No hay cupo en este evento";
        }
        return (mensajeError == "");


    }

}
