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
    public void BajarEvento(int id) 
    {
        throw new NotImplementedException();
    }
    public void ModificarEvento(int id)
    {
        throw new NotImplementedException();
    }
}
