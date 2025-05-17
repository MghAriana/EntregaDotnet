using System;

namespace CentroEventos.Aplicacion;

public class EventoDeportivoValidador(IRepositorioPersona repo)
{
    public bool Validar(EventoDeportivo eventoDeportivo, out string mensajeError)
    {
        mensajeError = "";
        if(string.IsNullOrWhiteSpace(eventoDeportivo.Nombre))
        {
            mensajeError = "Nombre del evento inválido.\n";
        }
        if(string.IsNullOrWhiteSpace(eventoDeportivo.Descripcion))
        {
            mensajeError += "Descrición del evento inválida.\n";
        }
        if(eventoDeportivo.FechaHoraInicio < DateTime.Now)
        {
            mensajeError += "Fecha y hora de inicio inválida.\n"; 
        }
        if(eventoDeportivo.CupoMaximo <= 0)
        {
            mensajeError += "Cupo máximo del evento inválido.\n";
        }
        if(eventoDeportivo.DuracionHoras <= 0)
        {
            mensajeError += "Duración del evento inválida. \n";
        }
        if(!repo.existeId(eventoDeportivo.ResponsableId)){
            mensajeError += "Responsable del evento inválido.\n";
        }
        return (mensajeError == "");
    }
}