using System;

namespace CentroEventos.Repositorios;
using CentroEventos.Aplicacion;

public class RepositorioEventoDeportivoTXT: IRepositorioEventoDeportivo
{
    readonly string _archivo = "eventos_deportivos.txt";
    public void AgregarEvento(EventoDeportivo edep)
    {
        //implementar
    }

    public bool ExistePersona(int responsableId)
    {
        //implementar
        return false;
    }

    public List<EventoDeportivo> ListarEventosConCupo(){

        List<EventoDeportivo> lista = new List<EventoDeportivo>();
        //implementar
        return lista;

    }
}
