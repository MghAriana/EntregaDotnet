using System;
namespace CentroEventos.Repositorios;

using System.Collections.Generic;
using CentroEventos.Aplicacion;

public class RepositorioReservaTXT (IRepositorioEventoDeportivo repoEVDE): IRepositorioReserva //preguntar
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
    public void Modificar(Reserva unareserva) //como el validador se encarga de dar a conocer si el id de la reserva existe y si la reservamodificada es corresta,
                                            // el modificar solo se encarga de subir la modificacion, sin importar si existe o no ya que eso se da como contemplado que es correcto.
    {//preguntar si es correcta la forma de pensarlo y si es necesario agregar un "traer reserva" para complementar o si todo el proceso de modificacion se realiza en el mismo Modificar del txt.
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
            if (e.CupoMaximo < cantcupo) listacupo.Add(e);
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
                Reserva reserva = new Reserva();
                reserva.Id = int.Parse(a[0]);
                reserva.Idpersona = int.Parse(a[1]);
                reserva.IdEven_Dep = int.Parse(a[2]);
                reserva.Fecha = DateTime.Parse(a[3]);
                reserva.EstadoAsistencia = Enum.Parse<Estado>(a[4]);
                listaR.Add(reserva);
                l = sr.ReadLine();
            }
        }
        return listaR;
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