using System;
namespace CentroEventos.Aplicacion;

/*  No puede modificarse un EventoDeportivo cuya FechaHoraInicio haya expirado 
    (es decir, no puede modificarse un evento pasado).
    Al modificar un EventoDeportivo, no puede establecerse la FechaHoraInicio con un valor anterior al actual 
    (es decir que sólo se permite si la fecha que va a registrarse es >= fecha actual).*/

public class EventoDeportivoModificacionUseCase(IRepositorioReserva repoR, IRepositorioEventoDeportivo repoE, EventoDeportivoValidador validador)
{
    private int desplegarMenu()
    {
        Console.WriteLine("<------------------- Modificar Evento Deportivo ------------------->");
        Console.WriteLine(" 1 - Editar nombre.");
        Console.WriteLine(" 2 - Editar descripcion.");
        Console.WriteLine(" 3 - Editar fecha y hora de inicio.");
        Console.WriteLine(" 4 - Editar duración (en horas). ");
        Console.WriteLine(" 5 - Editar cantidad máxima de participantes.");
        Console.WriteLine(" 6 - Editar id del responsable.");
        Console.WriteLine(" 7 - Volver al menú principal.");
        Console.WriteLine("<------------------------------------------------------------------>");
        Console.Write(" Ingrese una opción: "); int opcion = int.TryParse(Console.ReadLine(), out int opc) ? opc : -1;
        return opcion;
    }
    public void Ejecutar()
    {
        Console.Write("Ingrese id del evento: ");
        int id_evento = int.TryParse(Console.ReadLine(), out int id) ? id : -1;
        if (id_evento == -1)
        {
            throw new Exception("El id ingresado debe ser un numero entero mayor que 0.");
        }
        List<EventoDeportivo> lista = repoE.ListarEventos();
        EventoDeportivo? evento = lista.Find(e => e.Id == id);
        if (evento == null)
        {
            throw new Exception("No se encontró un evento con el id proporcionado.");
        }
        bool terminar = false;
        while (!terminar)
        {
            int opcion = this.desplegarMenu();
            switch (opcion)
            {
                case 1:
                    Console.Write("Nuevo nombre: ");
                    string? nuevoNombre = Console.ReadLine();
                    evento.Nombre = nuevoNombre;
                    break;
                case 2:
                    Console.Write("Nueva descripción: ");
                    string? nuevaDesc = Console.ReadLine();
                    evento.Descripcion = nuevaDesc;
                    break;
                case 3:
                    Console.Write("Nueva fecha y hora (ej: aaaa/mm/dd HH:mm): ");
                    DateTime nuevaFechaHora = DateTime.TryParse(Console.ReadLine(), out var fecha) ? fecha : DateTime.MinValue;
                    evento.FechaHoraInicio = nuevaFechaHora;
                    break;
                case 4:
                    Console.Write("Nueva duración (horas): ");
                    double nuevaDur = double.TryParse(Console.ReadLine(), out double ndur) ? ndur : 0;
                    evento.DuracionHoras = nuevaDur;
                    break;
                case 5:
                    Console.Write("Nuevo cupo máximo: ");
                    int nuevoCupo = int.TryParse(Console.ReadLine(), out int ncupo) ? ncupo : 0;
                    int cant = repoR.ContarReservas(id_evento);
                    if (cant > nuevoCupo)
                    {
                        throw new Exception("La cantidad de reservas ");
                    }
                    evento.CupoMaximo = nuevoCupo;
                    break;
                case 6:
                    Console.Write("Nuevo id responsable: ");
                    int nuevoRespo = int.TryParse(Console.ReadLine(), out int nrespo) ? nrespo : 0;
                    evento.ResponsableId = nuevoRespo;
                    break;
                case 7:
                    terminar = true;
                    break;
                default:
                    Console.WriteLine("Ingrese una opción válida.");
                    break;
            }
        }
        if (!validador.Validar(evento, out string mensajeError))
        {
            throw new Exception(mensajeError);
        }
        //repoE.ActualizarRepositorio(evento);

    }
}
