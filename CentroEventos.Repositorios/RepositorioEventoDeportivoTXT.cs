using System;

namespace CentroEventos.Repositorios;
using CentroEventos.Aplicacion;

public class RepositorioEventoDeportivoTXT: IRepositorioEventoDeportivo
{
    readonly string _archivo = "eventos_deportivos.csv";
    public void AgregarEvento(EventoDeportivo evento)
    {
        using var sw = new StreamWriter(_archivo, true);
        string[] linea = {  $"{evento.Id}", 
                            $"{evento.Nombre}", 
                            $"{evento.Descripcion}", 
                            $"{evento.FechaHoraInicio}",
                            $"{evento.DuracionHoras}",
                            $"{evento.CupoMaximo}",
                            $"{evento.ResponsableId}" };
        sw.WriteLine(string.Join(",",linea)); //Preguntar si se puede hacer esto. 
    }

    public void BajarEvento(int id){
        //implementar
    }

    public bool ExisteResponsable(int responsableId)
    {
        bool existe = false;
        using var sr = new StreamReader(_archivo, true);
        string? linea;
        while((linea = sr.ReadLine()) != null && !existe)
        {
            string[] campo = linea.Split(','); 
            // campos = [ Id, Nombre, Descripcion, FechaHoraInicio, DuracionHoras, CupoMaximo, ResponsableId ]
            if(int.Parse(campo[6]) == responsableId) {
                existe = true;
            }   
        }
        sr.Dispose();
        return existe;
    }

    public List<EventoDeportivo> ListarEventosConCupo(){

        List<EventoDeportivo> lista = new List<EventoDeportivo>();
        //implementar
        return lista;

    }
}
