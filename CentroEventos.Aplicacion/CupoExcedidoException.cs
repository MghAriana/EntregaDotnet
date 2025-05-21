using System;

namespace CentroEventos.Aplicacion;

public class CupoExcedidoException:Exception
{
     public CupoExcedidoException()
            : base("no hay cupos disponibles") { } 
    public CupoExcedidoException(string mensaje)
        : base(mensaje) { }

    public CupoExcedidoException(string mensaje, Exception innerException)
    {
        
    }
}
