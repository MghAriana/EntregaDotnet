using System;
using System.Globalization;

namespace CentroEventos.Aplicacion;

public class EventoDeportivoAltaUseCase(IRepositorioEventoDeportivo repoE,
                                        EventoDeportivoValidador validador, IRepositorioID repoID) //inyección de dependencias
{
    private EventoDeportivo cargarEvento()
    {
        Console.Write("Nombre: ");
        string? nombre = Console.ReadLine();
        Console.Write("Descripción: ");
        string? descripcion = Console.ReadLine();
        Console.WriteLine("Ingrese una fecha y hora (ej: aaaa/mm/dd 14:36):");
        DateTime fechaHoraInicio = DateTime.TryParse(Console.ReadLine(), out var fecha) ? fecha : DateTime.MinValue;
        Console.Write("Duración en Horas: ");
        double duracionHoras = double.TryParse(Console.ReadLine(), out var duracion) ? duracion : -1;
        Console.Write("Cupo máximo: ");
        int cupoMaximo = int.TryParse(Console.ReadLine(), out var cupo) ? cupo : -1;
        Console.Write("Id del Responsable: ");
        int responsableId = int.TryParse(Console.ReadLine(), out var id) ? id : -1;
        
        return new EventoDeportivo(repoID.GenerarId("EventoDeportivo"), nombre, descripcion,
                                    fechaHoraInicio, duracionHoras, cupoMaximo, responsableId);
    }

    public void Ejecutar()
    {
        EventoDeportivo evento = this.cargarEvento();
        if (!validador.Validar(evento, out string mensajeError))
        {
            throw new Exception(mensajeError);
        }
        repoE.AgregarEvento(evento);
    }
}
