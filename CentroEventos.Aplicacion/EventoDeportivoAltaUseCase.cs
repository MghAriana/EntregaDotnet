using System;
using System.Globalization;

namespace CentroEventos.Aplicacion;

public class EventoDeportivoAltaUseCase(IRepositorioEventoDeportivo repoE,
                                        EventoDeportivoValidador validador, IRepositorioID repoID) //inyección de dependencias
{
    private void cargarEvento(EventoDeportivo evento)
    {
        Console.Write("Nombre: "); evento.Nombre = Console.ReadLine();
        Console.Write("Descripción: "); evento.Descripcion = Console.ReadLine();
        Console.WriteLine("Ingrese una fecha y hora (ej: aaaa/mm/dd 14:36):");
        evento.FechaHoraInicio =DateTime.TryParse(Console.ReadLine(), out var fecha) ? fecha: DateTime.MinValue;
        Console.Write("Duración en Horas: ");
        evento.DuracionHoras = double.TryParse(Console.ReadLine(), out var duracion) ? duracion: -1;
        Console.Write("Cupo máximo: ");
        evento.CupoMaximo = int.TryParse( Console.ReadLine(), out var cupo) ? cupo: -1;
        Console.Write("Id del Responsable: ");
        evento.ResponsableId = int.TryParse(Console.ReadLine(), out var responsableId) ? responsableId: -1;
    }

    public void Ejecutar(EventoDeportivo evento) //recibe un objeto de tipo EventoDeportivo
    {
        this.cargarEvento(evento);
        if (!validador.Validar(evento, out string mensajeError))
        {
            throw new Exception(mensajeError);
        }
        evento.Id = repoID.GenerarId("EventoDeportivo");
        repoE.AgregarEvento(evento);
    }
}
