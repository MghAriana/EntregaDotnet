using System;
using System.Data.Common;

namespace CentroEventos.Aplicacion;

public class ListarAsistenciaAEventosUseCase (IRepositorioReserva repo)
{
    public List<Estado> Ejecutar( int idEvento){

        return repo.ListarAsistencia(idEvento); //preguntar
    }
}
