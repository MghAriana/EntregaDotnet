using System;

namespace CentroEventos.Aplicacion;

public class EventoDeportivoAltaUseCase(IRepositorioEventoDeportivo repo,
                                        EventoDeportivoValidador validador) //inyección de dependencias
{
    public void Ejecutar(EventoDeportivo eventoDeportivo) //recibe un objeto de tipo EventoDeportivo
    {
        if(!validador.Validar(eventoDeportivo, out string mensajeError)) 
        {
            throw new Exception(mensajeError);
        }
        repo.AgregarEvento(eventoDeportivo); 
    }
}
