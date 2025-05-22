using System;
namespace CentroEventos.Repositorios;

using System.Collections.Generic;
using CentroEventos.Aplicacion;

public class RepositorioReservaTXT (IRepositorioEventoDeportivo repoEVDE, IRepositorioPersona repoP): IRepositorioReserva//preguntar
{
    readonly string _archReserva = "Reservas.txt";
    public void AgregarReserva(Reserva unareserva)
    {
        using (var sw = new StreamWriter(_archReserva, true))
        {
            string[] vec = {  $"{unareserva.Id_reserva}",
                                $"{unareserva.Idpersona}",
                                $"{unareserva.IdEven_Dep}",
                                $"{unareserva.Fecha}",
                                $"{unareserva.EstadoAsistencia}"};
            sw.WriteLine(string.Join(",", vec));
        }
    }
    public void RealizarBaja(int idReserva)
    {
        List<Reserva> listaR = this.ListarReserva();
        if (listaR.Count() == 0)
        {
            throw new Exception("No hay reservas");
        }
        int i = 0;
        bool existe = false;
        while (i < listaR.Count() && !existe)
        {
            if (listaR[i].Id_reserva == idReserva)
            {
                listaR.RemoveAt(i);
                Console.WriteLine("Eliminado con exito");
                existe = true;
            }
            else { i++; }
        }
        using (var sw = new StreamWriter(_archReserva, false))
        {
            foreach (Reserva reserva in listaR)
            {
                string l = $"{reserva.Id_reserva},{reserva.Idpersona},{reserva.IdEven_Dep},{reserva.Fecha},{reserva.EstadoAsistencia}";
                sw.WriteLine(l);
            }
        }
    }

    public void Modificar(Reserva unareserva) //como el validador se encarga de dar a conocer si el id de la reserva existe y si la reservamodificada es corresta,
                                              // el modificar solo se encarga de subir la modificacion, sin importar si existe o no ya que eso se da como contemplado que es correcto.
    {//preguntar si es correcta la forma de pensarlo y si es necesario agregar un "traer reserva" para complementar o si todo el proceso de modificacion se realiza en el mismo Modificar del txt.
        List<Reserva> reservas = this.ListarReserva();
        int i = 0;
        bool modificado = false;
        while (i < reservas.Count() && !modificado)
        {
            if (reservas[i].Id_reserva == unareserva.Id_reserva)
            {
                reservas[i] = unareserva;
                modificado = true;
            }
            else { i++; }
        }
        using (var sw = new StreamWriter(_archReserva, false))
        {
            foreach (Reserva r in reservas)
            {
                string l = $"{r.Id_reserva},{r.Idpersona},{r.IdEven_Dep},{r.Fecha},{r.EstadoAsistencia}";
                sw.WriteLine(l);
            }
        }
    }
    public bool ExisteResposable(int IdResponsable)
    {
        List<Persona> listaP = repoP.ListarPersonas();
        if (listaP.Count() == 0)
        {
            throw new Exception("No hay reservas");
        }
        int i = 0;
        while (i < listaP.Count())
        {
            if (listaP[i].Id == IdResponsable)
            {
                return true;
            }
            else { i++; }
        }
        return false;
    }
    public bool existenReservasAsociadasAlEvento(int idEvento)
    {
        List<Reserva> listaR = this.ListarReserva();
        Reserva? reserva = listaR.Find(r => r.IdEven_Dep == idEvento);
        return reserva == null;
    }

    public bool existeReservaRegistrada(int id_persona, int id_evento)
    {
        List<Reserva> listaR = this.ListarReserva();
        Reserva? filtro = listaR.Find(r => r.IdEven_Dep == id_evento && r.Idpersona == id_persona);
        if (filtro == null) return false;
        return true;
    }
    public bool existeReservaAsociadaAPersona(int idpersona)
    {
        List<Reserva> listaR = this.ListarReserva();
        Reserva? reserva = listaR.Find(r => r.Idpersona == idpersona);
        return reserva!=null;
    }

    public bool ExisteCupo(int idEvento)
    {
        List<EventoDeportivo> listaE = repoEVDE.ListarEventos();
        EventoDeportivo? evento = listaE.Find(e => e.Id_evento == idEvento);
        if (evento != null)
        {
            int cant_reservas = this.ContarReservasSegunEvento(idEvento);
            Console.WriteLine($"cantidad de reservas asociadas al evento {idEvento}: {cant_reservas}");
            Console.WriteLine($"Cupo máximo del evento: {evento.CupoMaximo}");
            return evento.CupoMaximo > cant_reservas;
        }
        return false; 
    }
    public List<EventoDeportivo> ListarEventosConCupo()
    {
        List<EventoDeportivo> eventos = repoEVDE.ListarEventos();
        List<EventoDeportivo> listaCupo = new List<EventoDeportivo>();
        foreach (EventoDeportivo e in eventos)
        {
            if (e.CupoMaximo > ContarReservasSegunEvento(e.Id_evento))
            {
                listaCupo.Add(e);
            }
        }
        if (listaCupo == null)
        {
            throw new Exception("No hay eventos con cupo disponible.");   
        }
        return listaCupo;

    }
    public List<Reserva> ListarReserva()
    {
        List<Reserva> listaR = new List<Reserva>();
        using var sr = new StreamReader(_archReserva, true);
        string? linea = sr.ReadLine();
        while (!string.IsNullOrEmpty(linea))
        {
            string[] a = linea.Split(",");
            Reserva reserva = new Reserva(int.Parse(a[0]), int.Parse(a[1]), int.Parse(a[2]), DateTime.Parse(a[3]), Enum.Parse<Estado>(a[4]));
            listaR.Add(reserva);
            linea = sr.ReadLine();
        }
        sr.Dispose();
        return listaR;
    }
public List<Persona> ListarAsistencia(int idEvento)
{
    List<EventoDeportivo> eventos = repoEVDE.ListarEventos();
    EventoDeportivo? even = eventos.Find(e => e.Id_evento == idEvento); 
    List<Reserva> reserva = this.ListarReserva();
    List<Persona> personas = repoP.ListarPersonas();
    List<Persona> Asistentes = new List<Persona>();

        foreach (Reserva r in reserva)
        {
            if (r.IdEven_Dep == idEvento && r.EstadoAsistencia == Estado.Presente)
            {
                foreach (Persona p in personas)
                {
                    if (r.Idpersona == p.Id)
                    {
                        Asistentes.Add(p);
                    }
                }
            }
            
        }
    return Asistentes;          
    }
    public int ContarReservasSegunEvento(int id_evento)
    {
        List<Reserva> listaR = this.ListarReserva();
        int cant = 0;
        int i = 0;
        while (i < listaR.Count())
        {
            if (listaR[i].IdEven_Dep == id_evento) cant++;
            i++;
        }
        return cant; 
    }
}