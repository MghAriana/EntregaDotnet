using System;

namespace CentroEventos.Aplicacion;

public class ListarEventosConCupoDisponibleUseCase(IRepositorioReserva repo)
{
    public List<EventoDeportivo>? Ejecutar(){
        
        return repo.ListarEventosConCupo();
    }
}