using System;

namespace CentroEventos.Repositorios;
using CentroEventos.Aplicacion;

public class RepositorioEventoDeportivoTXT : IRepositorioEventoDeportivo
{
    readonly string _archivoED = "eventos_deportivos.txt";
    public void AgregarEvento(EventoDeportivo evento)
    {
        using var sw = new StreamWriter(_archivoED, true);
        // Genero un vector de string ["id","nombre","descripcion","fechaHoraInicio","duracion","cupo","responsabe"]
        string[] linea = {  $"{evento.Id_evento}",
                            $"{evento.Nombre}",
                            $"{evento.Descripcion}",
                            $"{evento.FechaHoraInicio}",
                            $"{evento.DuracionHoras}",
                            $"{evento.CupoMaximo}",
                            $"{evento.ResponsableId}" };
        sw.WriteLine(string.Join(",", linea)); // Creo un string con todos los campos separados por "," y lo cargo en el archivo. 
        sw.Dispose();
    }
    public void BajarEvento(int id_evento)
    {
        List<EventoDeportivo> lista = this.ListarEventos();
        int i = 0;
        bool encontre = false;
        while (i < lista.Count() && !encontre)
        {
            if (lista[i].Id_evento == id_evento)
            {
                lista.RemoveAt(i);
                encontre = true;
            }
            i++;
        }
        if (encontre) Console.WriteLine("Evento eliminado");

        using var sw = new StreamWriter(_archivoED, false);
        foreach (EventoDeportivo evento in lista)
        {
            string linea = $"{evento.Id_evento},{evento.Nombre},{evento.Descripcion},{evento.FechaHoraInicio},{evento.DuracionHoras},{evento.CupoMaximo},{evento.ResponsableId}";
            sw.WriteLine(linea);
        }
        sw.Dispose();
    }
    public List<EventoDeportivo> ListarEventos()
    {
        List<EventoDeportivo> lista = new List<EventoDeportivo>();
        using var sr = new StreamReader(_archivoED);
        string? linea = sr.ReadLine();
        while (!string.IsNullOrEmpty(linea))
        {
            string[] c = linea.Split(",");
            EventoDeportivo evento = new EventoDeportivo(int.Parse(c[0]), c[1], c[2], DateTime.Parse(c[3]), double.Parse(c[4]), int.Parse(c[5]), int.Parse(c[6]));
            lista.Add(evento);
            linea = sr.ReadLine();
        }
        sr.Dispose();
        return lista;
    }
    private int desplegarMenu()
    {
        Console.WriteLine("<------------------- Modificar Evento Deportivo ------------------->");
        Console.WriteLine(" 1 - Editar nombre.                                                 ");
        Console.WriteLine(" 2 - Editar descripcion.                                            ");
        Console.WriteLine(" 3 - Editar fecha y hora de inicio.                                 ");
        Console.WriteLine(" 4 - Editar duración (en horas).                                    ");
        Console.WriteLine(" 5 - Editar cantidad máxima de participantes.                       ");
        Console.WriteLine(" 6 - Editar id del responsable.                                     ");
        Console.WriteLine(" 7 - Volver al menú principal.                                      ");
        Console.WriteLine("<------------------------------------------------------------------>");
        Console.Write(" Ingrese una opción: "); int opcion = int.TryParse(Console.ReadLine(), out int opc) ? opc : -1;
        return opcion;
    }

    public void ModificarEvento(int id_evento, IRepositorioReserva repoR, EventoDeportivoValidador validador)
    {
        List<EventoDeportivo> lista = this.ListarEventos();
        EventoDeportivo? evento = lista.Find(e => e.Id_evento == id_evento); // Busco el evento a modificar.
        if (evento == null)
        {
            throw new Exception("No se encontró un evento con el id proporcionado.");
        }
        bool terminar = false;
        if (evento.FechaHoraInicio < DateTime.Now)
        {
            throw new Exception("No puede modificar un evento que ya finalizó.");
        }
        while (!terminar)
        {
            int opcion = this.desplegarMenu();
            switch (opcion)
            {
                case 1:
                    Console.Write("Nuevo nombre: ");
                    evento.Nombre = Console.ReadLine() ?? "";
                    break;
                case 2:
                    Console.Write("Nueva descripción: ");
                    evento.Descripcion = Console.ReadLine() ?? "";
                    break;
                case 3:
                    Console.Write("Nueva fecha y hora (ej: aaaa/mm/dd HH:mm): ");
                    evento.FechaHoraInicio = DateTime.TryParse(Console.ReadLine(), out var fecha) ? fecha : DateTime.MinValue;
                    break;
                case 4:
                    Console.Write("Nueva duración (horas): ");
                    evento.DuracionHoras = double.TryParse(Console.ReadLine(), out double ndur) ? ndur : 0;
                    break;
                case 5:
                    Console.Write("Nuevo cupo máximo: ");
                    int nuevoCupo = int.TryParse(Console.ReadLine(), out int ncupo) ? ncupo : 0;
                    int cant = repoR.ContarReservasSegunEvento(id_evento);
                    if (cant > nuevoCupo)
                    {
                        throw new Exception($"El evento {id_evento} cuenta con {cant} reservas registradas, no es posible bajar el cupo a {nuevoCupo}.");
                    }
                    evento.CupoMaximo = nuevoCupo;
                    break;
                case 6:
                    Console.Write("Nuevo id responsable: ");
                    evento.ResponsableId = int.TryParse(Console.ReadLine(), out int nrespo) ? nrespo : 0;
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
            throw new ValidacionException(mensajeError);
        }
        this.BajarEvento(id_evento); // Doy de baja el evento. 
        this.AgregarEvento(evento); // Agrego el evento modificado.
    }

    public bool esResponsableDeEvento(int id)
    {
        List<EventoDeportivo> lista = this.ListarEventos();
        EventoDeportivo? evento = lista.Find(e => e.ResponsableId == id);
        return (evento != null); // si existe responsable retorna true, sino, retorna false.
    }
}
