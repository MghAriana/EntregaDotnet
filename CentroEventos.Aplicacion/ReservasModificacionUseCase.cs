using System;

namespace CentroEventos.Aplicacion;

public class ReservasModificacionUseCase (IRepositorioReserva repo, ReservasValidador validador)
{
    public void Ejecutar(Reserva reservamodificada)
    {
        if (!repo.existeLaReserva(reservamodificada.Id))
        {
            throw new Exception("No existe la reserva");
        }
        if (!validador.Validar(reservamodificada, out string error))
        {
            throw new Exception("Hay un error en la modificacion: " + error);
        }
        repo.Modificar(reservamodificada);
    }
}
