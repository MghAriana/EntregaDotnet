using System;

namespace CentroEventos.Aplicacion;

public class ListarEventoDeportivoUseCase(IRepositorioEventoDeportivo repoED)
{
    public List<EventoDeportivo> Ejecutar(){
       return repoED.ListarEventos();
    }
}
