using System;

namespace CentroEventos.Aplicacion;

public class PersonaBajaUseCase(IRepositorioPersona repoPer, IRepositorioReserva reserva,IRepositorioEventoDeportivo repoevento)
{
    public void Ejecutar(Persona persona)
    {
        if (reserva.existenReservaAsociadaAPersona(persona.Id) &&repoevento.existeResponsable(persona.Id) )
        {
            throw new Exception("no se puede eliminar una persona porque tiene una reserva asociada");
        }
        
        else
        {
            repoPer.eliminarPersona(persona.Id);
        }
    }
}
