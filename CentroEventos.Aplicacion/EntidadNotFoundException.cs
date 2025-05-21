using System;

namespace CentroEventos.Aplicacion;

public class EntidadNotFoundException:Exception
{
    public EntidadNotFoundException()
            : base("el id ingresado no existe") { } 
    public EntidadNotFoundException(string mensaje)
        : base(mensaje) { }

    /*public EntidadNotFoundException(string mensaje, Exception innerException)
    {
        
    }*/
}
