using System;

namespace CentroEventos.Aplicacion;

public class OperacionInvalidaException:Exception
{
    public OperacionInvalidaException()
            : base("Operacion invalida") { } 
    public OperacionInvalidaException(string mensaje)
        : base(mensaje) { }

    public OperacionInvalidaException(string mensaje, Exception innerException)
    {
        
    }
}
