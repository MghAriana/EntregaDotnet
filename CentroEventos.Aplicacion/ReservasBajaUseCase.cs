using System;

namespace CentroEventos.Aplicacion;

public class ReservasBajaUseCase (IRepositorioReserva repo)
{
    public void Ejecutar (int idReserva){
        repo.RealizarBaja(idReserva);
    }
}
//csv