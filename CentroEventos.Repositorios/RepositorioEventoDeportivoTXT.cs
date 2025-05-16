using System;

namespace CentroEventos.Repositorios;
using CentroEventos.Aplicacion;

public class RepositorioEventoDeportivoTXT: IRepositorioEventoDeportivo
{
    readonly string _archivoED = "eventos_deportivos.txt";
    public void AgregarEvento(EventoDeportivo evento)
    {
        using var sw = new StreamWriter(_archivoED, true);
        // Genero un vector de string ["id","nombre","descripcion","fechaHoraInicio","duracion","cupo","responsabe"]
        string[] linea = {  $"{evento.Id}", 
                            $"{evento.Nombre}", 
                            $"{evento.Descripcion}", 
                            $"{evento.FechaHoraInicio}",
                            $"{evento.DuracionHoras}",
                            $"{evento.CupoMaximo}",
                            $"{evento.ResponsableId}" };
        sw.WriteLine(string.Join(",",linea)); // Creo un string con todos los campos separados por "," y lo cargo en el archivo. 
        sw.Dispose();
    }
    public void BajarEvento(int id_evento) 
    {
        List<EventoDeportivo> lista = this.ListarEventos();
        if(lista.Count() == 0)
        {
            throw new Exception("No hay eventos cargados.");
        }
        int i = 0;
        bool encontre = false;
        while(i < lista.Count() && !encontre){
            if(lista[i].Id == id_evento)
            {   
                lista.RemoveAt(i);
                encontre = true;
            }
            else i++;
        }
        if(encontre) Console.WriteLine("Evento eliminado");

        using var sw = new StreamWriter(_archivoED, false);
        foreach(EventoDeportivo evento in lista){
            string linea = $"{evento.Id},{evento.Nombre},{evento.Descripcion},{evento.FechaHoraInicio},{evento.DuracionHoras},{evento.CupoMaximo},{evento.ResponsableId}";
            sw.WriteLine(linea);
        }
        sw.Dispose();
    }
    public void ModificarEvento(int id_evento)
    {
        throw new NotImplementedException();
    }

    public List<EventoDeportivo> ListarEventos()
    {
        List<EventoDeportivo> lista = new List<EventoDeportivo>();
        using var sr = new StreamReader(_archivoED);
        string? linea = sr.ReadLine();
        while(!string.IsNullOrEmpty(linea))
        { 
            string[] c = linea.Split(","); 
            EventoDeportivo evento = new EventoDeportivo();
            evento.Id = int.Parse(c[0]);
            evento.Nombre = c[1];
            evento.Descripcion = c[2];
            evento.FechaHoraInicio = DateTime.Parse(c[3]);
            evento.DuracionHoras = double.Parse(c[4]);
            evento.CupoMaximo = int.Parse(c[5]);
            evento.ResponsableId = int.Parse(c[6]);

            lista.Add(evento);
            linea = sr.ReadLine();
        }
        sr.Dispose();
        return lista;
    }

}
