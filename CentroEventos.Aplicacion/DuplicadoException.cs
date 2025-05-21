using System;

namespace CentroEventos.Aplicacion;

public class DuplicadoException:Exception
{
    public DuplicadoException()
            : base("Informacion existente ") { } 
    public DuplicadoException(string mensaje)
        : base(mensaje) { }

    public DuplicadoException(string mensaje, Exception innerException)
    {
        
    }
}
