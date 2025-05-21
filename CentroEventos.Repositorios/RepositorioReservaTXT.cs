using System;
namespace CentroEventos.Repositorios;

using System.Collections.Generic;
using CentroEventos.Aplicacion;

public class RepositorioReservaTXT (IRepositorioEventoDeportivo repoEVDE, IRepositorioPersona repoP): IRepositorioReserva//preguntar
{
    readonly string _archReserva = "Reservas.txt";
    public void AgregarReserva(Reserva unareserva)
    {
        using (var sw = new StreamWriter(_archReserva, true)){
            string[] vec = {  $"{unareserva.Id}",
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
            if (listaR[i].Id == idReserva)
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
                string l = $"{reserva.Id},{reserva.Idpersona},{reserva.IdEven_Dep},{reserva.Fecha},{reserva.EstadoAsistencia}";
                sw.WriteLine(l);
            }
        }
    }
    public void Modificar(Reserva unareserva) 
    {
        List<Reserva> reservas = this.ListarReserva();
        int i = 0;
        bool modificado = false;
        while (i < reservas.Count() && !modificado)
        {
            if (reservas[i].Id == unareserva.Id)
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
                string l = $"{r.Id},{r.Idpersona},{r.IdEven_Dep},{r.Fecha},{r.EstadoAsistencia}";
                sw.WriteLine(l);
            }
        }
    }
    public bool ExisteResposable(int IdResponsable)
    {
        List<Reserva> listaR = this.ListarReserva();
        if (listaR.Count() == 0)
        {
            throw new Exception("No hay reservas");
        }
        int i = 0;
        while (i < listaR.Count())
        {
            if (listaR[i].Idpersona == IdResponsable)
            {
                return true;
            }
            else { i++; }
        }
        return false;
    }
    public bool existenReservas(int idEvento)
    {
       List<Reserva> listaR = this.ListarReserva(); 
        if (listaR.Count() == 0)
        {
            throw new Exception("No hay reservas");
        }
        int i = 0;
        while (i < listaR.Count())
        {
            if (listaR[i].IdEven_Dep == idEvento)
            {
                return true;
            }
            else { i++; }
        }
        return false;
    }
    public bool existeReservaAsociadaAPersona(int idpersona)
    {
       List<Reserva> listaR = this.ListarReserva(); 
        if (listaR.Count() == 0)
        {
            throw new Exception("No hay reservas");
        }
        int i = 0;
        while (i < listaR.Count())
        {
            if (listaR[i].Idpersona== idpersona)
            {
                return true;
            }
            else { i++; }
        }
        return false;
    }

    public bool ExisteCupo(int idEvento)
    {
        List<EventoDeportivo> lcupo = ListarEventosConCupo();
        foreach (EventoDeportivo e in lcupo)
        {
            if (e.Id == idEvento) return true;
        }
        return false;
    }
    public List<EventoDeportivo> ListarEventosConCupo()
    {
        List<EventoDeportivo> eventos = repoEVDE.ListarEventos();
        if(eventos.Count() == 0) throw new Exception("No hay eventos cargados.");

        List<Reserva> reservas = this.ListarReserva();
        if(reservas.Count() == 0) throw new Exception("No hay eventos cargados.");

        List<EventoDeportivo> listacupo = new List<EventoDeportivo>();
        foreach (EventoDeportivo e in eventos)
        {
            int cantcupo = 0;
            foreach (Reserva r in reservas)
            {
                if (r.IdEven_Dep == e.Id)
                {
                    cantcupo++;
                }
            }
            if (e.CupoMaximo > cantcupo) listacupo.Add(e);
        }
        return listacupo;
        
    }
    public List<Reserva> ListarReserva()
    {
        List<Reserva> listaR = new List<Reserva>();
        using (var sr = new StreamReader(_archReserva))
        {
            string? l = sr.ReadLine();
            while (string.IsNullOrEmpty(l))
            {
                string[] a = l.Split(",");
                Reserva reserva = new Reserva(int.Parse(a[0]), int.Parse(a[1]),int.Parse(a[2]),DateTime.Parse(a[3]),Enum.Parse<Estado>(a[4]));
                listaR.Add(reserva);
                l = sr.ReadLine();
            }
        }
        return listaR;
    }
public List<Persona> ListarAsistencia(int idEvento)
{
    List<EventoDeportivo> eventos = repoEVDE.ListarEventos();
    EventoDeportivo? even = eventos.Find(e => e.Id == idEvento); 
    if (even == null)
        {
            throw new Exception("No existe el evento");
        }
    if (even.FechaHoraInicio < DateTime.Now)
    {
        throw new Exception("El evento esta pendiente");
    }
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

    public bool existeLaReserva(int idReserva)
    {
        List<Reserva> listaR = this.ListarReserva();
        if (listaR.Count() == 0)
        {
            throw new Exception("No hay reservas");
        }
        int i = 0;
        while (i < listaR.Count())
        {
            if (listaR[i].Id == idReserva)
            {
                return true;
            }
            else { i++; }
        }
        return false;
    }
}