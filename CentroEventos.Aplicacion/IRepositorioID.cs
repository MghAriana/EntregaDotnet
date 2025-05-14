using System;

namespace CentroEventos.Aplicacion;

public interface IRepositorioID
{
    public int GenerarId(string nom_clase);
}
