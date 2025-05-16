using System;
using System.Security.Cryptography;

namespace CentroEventos.Aplicacion;

public class EventoDeportivo
{
    private int _Id; // único, debe ser autoincremental gestionado por el repositorio) 
    private string? _Nombre;// ej: "Clase de Spinning Avanzado", "Partido final de Vóley"
    private string? _Descripcion;
    private DateTime _FechaHoraInicio; // DateTime - Fecha y hora exactas de inicio del evento
    private double _DuracionHoras; // Duración del evento en horas, ej: 1.5 para una hora y media
    private int _CupoMaximo; // Cantidad máxima de participantes permitidos
    private int _ResponsableId; // Id de la Persona a cargo del evento

    public EventoDeportivo( IRepositorioID repo, string nombre, string descripcion, 
                            DateTime dateTime, double horas, int cupo, int id_persona)
    {
        _Id = repo.GenerarId("EventoDeportivo");
        _Nombre = nombre;
        _Descripcion = descripcion;
        _FechaHoraInicio = dateTime;
        _DuracionHoras = horas;
        _CupoMaximo = cupo;
        _ResponsableId = id_persona;
    }
    public EventoDeportivo() {}
    public int Id{
        get { return _Id; }
        set { _Id = value; }
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

    public override string ToString(){
        return $" Evento {Id} - '{Nombre}': \n Descripcion: {Descripcion} - Fecha y Hora: {FechaHoraInicio.ToString()} ";
    }
}