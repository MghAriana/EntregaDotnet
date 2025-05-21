using System;
using System.Data.Common;

namespace CentroEventos.Aplicacion;

public class ListarAsistenciaAEventosUseCase (IRepositorioReserva repo)
{
    public List<Persona> Ejecutar( int idEvento){

        return repo.ListarAsistencia(idEvento); //preguntar
    }
}
