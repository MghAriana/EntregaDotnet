using System;

namespace CentroEventos.Aplicacion;

public class EventoDeportivo (IRepositorioID repo)
{
    private int _Id = repo.GenerarId("EventoDeportivo"); // único, debe ser autoincremental gestionado por el repositorio) 
    private string? _Nombre;// ej: "Clase de Spinning Avanzado", "Partido final de Vóley"
    private string? _Descripcion;
    private DateTime _FechaHoraInicio; // DateTime - Fecha y hora exactas de inicio del evento
    private double _DuracionHoras; // Duración del evento en horas, ej: 1.5 para una hora y media
    private int _CupoMaximo; // Cantidad máxima de participantes permitidos
    private int _ResponsableId; // Id de la Persona a cargo del evento

    public int Id{
        get { return _Id; }
    }

    public string? Nombre {
        get { return _Nombre; }
        set { _Nombre = value; }
    }

    public string? Descripcion {
        get { return _Descripcion; }
        set { _Descripcion = value; }
    }

    public DateTime FechaHoraInicio {
        get { return _FechaHoraInicio; }
        set { _FechaHoraInicio = value; }
    }

    public double DuracionHoras {
        get { return _DuracionHoras; }
        set { _DuracionHoras = value; }
    }

    public int CupoMaximo {
        get { return _CupoMaximo; }
        set { _CupoMaximo = value; }
    }

    public int ResponsableId {
        get { return _ResponsableId; }
        set { _ResponsableId = value; }
    }
}