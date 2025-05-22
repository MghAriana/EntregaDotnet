using System;

namespace CentroEventos.Aplicacion;

public class ServicioDeAutorizacionProvisorio : IServicioAutorizacion
{
    public bool PoseeElPermiso(int IdUsuario, Permiso permiso)
    {
        if (IdUsuario == 1) return true;
        else return false; 
    }
}
