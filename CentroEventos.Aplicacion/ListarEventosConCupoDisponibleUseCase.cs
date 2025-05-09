using System;

namespace CentroEventos.Aplicacion;

public class ListarEventosConCupoDisponibleUseCase(IRepositorioEventoDeportivo repo) //,IRepositorioReserva
{
    public List<EventoDeportivo>? Ejecutar(){
        
        return repo.ListarEventosConCupo();
    }
}