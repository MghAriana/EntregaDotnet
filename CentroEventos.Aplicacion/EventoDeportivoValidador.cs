using System;

namespace CentroEventos.Aplicacion;

public class EventoDeportivoValidador(IRepositorioPersona repo)
{
    public bool Validar(EventoDeportivo eventoDeportivo, out string mensajeError)
    {
        mensajeError = "";
        if(string.IsNullOrWhiteSpace(eventoDeportivo.Nombre))
        {
            mensajeError = "El nombre del evento no es válido.\n";
        }
        if(string.IsNullOrWhiteSpace(eventoDeportivo.Descripcion))
        {
            mensajeError += "La descrición del evento no es válida.\n";
        }
        if(eventoDeportivo.FechaHoraInicio == DateTime.MinValue || eventoDeportivo.FechaHoraInicio < DateTime.Now)
        {
            mensajeError += "La fecha/hora de inicio del evento no es válida.\n"; 
        }
        if(eventoDeportivo.CupoMaximo <= 0)
        {
            mensajeError += "El cupo máximo del evento no es válido.\n";
        }
        if(eventoDeportivo.DuracionHoras <= 0)
        {
            mensajeError += "La duración del evento no es válida. \n";
        }
        if(eventoDeportivo.ResponsableId <= 0 || !repo.existeId(eventoDeportivo.ResponsableId)){
            mensajeError += "El Id del responsable del evento no es válido.\n";
        }
        return (mensajeError == "");
    }
}