using System;

namespace CentroEventos.Aplicacion;

public class PersonaBajaUseCase(IRepositorioPersona repoPer, IRepositorioReserva reserva,IRepositorioEventoDeportivo repoevento)
{
    public void Ejecutar(int id_persona)
    {
        if (reserva.existeReservaAsociadaAPersona(id_persona) &&repoevento.existeResponsable(id_persona) )
        {
            throw new Exception("no se puede eliminar una persona porque tiene una reserva asociada");
        }
        
        else
        {
            repoPer.eliminarPersona(id_persona);
        }
    }
}
