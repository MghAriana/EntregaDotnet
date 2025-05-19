using System;

namespace CentroEventos.Aplicacion;

public class ReservasAltaUseCase(IRepositorioReserva repo, ReservasValidador validacion)
{
    public void Ejecutar(Reserva unareserva)
    {
        if (!validacion.Validar(unareserva, out string error))
        {
            throw new Exception("Error al subir la reserva: " + error);
        }
            repo.AgregarReserva(unareserva);
    }
}
