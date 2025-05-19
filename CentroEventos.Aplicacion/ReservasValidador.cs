using System;

namespace CentroEventos.Aplicacion;

public class ReservasValidador(IRepositorioReserva repoReserva,IRepositorioPersona Ipersona, IRepositorioEventoDeportivo Ieventos)
{
    public bool Validar(Reserva unareserva, out string mensajeError){

        mensajeError = "";

        if (!Ipersona.existeID((int)unareserva.Idpersona))
        {
            mensajeError = "El dni no existe. \n";
        }
        if (!repoReserva.ExisteResposable((int)unareserva.Idpersona))
        {
            mensajeError = "La persona no reservo. \n";
        }
        if (repoReserva.ExisteResposable((int)unareserva.Idpersona) && repoReserva.existenReservas((int)unareserva.IdEven_Dep))
        {
            mensajeError = "Ya existe reserva para este evento /n";
        }
        if (!repoReserva.ExisteCupo(unareserva.Id))
        {
            mensajeError = "No hay cupo en este evento";
        }

        return mensajeError == "";


    }

}
