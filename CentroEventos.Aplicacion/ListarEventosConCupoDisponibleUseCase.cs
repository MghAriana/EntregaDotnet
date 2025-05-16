using System;

namespace CentroEventos.Aplicacion;

public class ListarEventosConCupoDisponibleUseCase(IRepositorioReserva repo) //,IRepositorioReserva
{
    public List<EventoDeportivo>? Ejecutar(){
        
        return repo.ListarEventosConCupo();
    }
}