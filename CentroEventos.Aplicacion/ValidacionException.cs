using System;

namespace CentroEventos.Aplicacion;

public class ValidacionException:Exception
{
    public ValidacionException()
            : base("tiene un formato incorrecto") { } 
    public ValidacionException(string mensaje)
        : base(mensaje) { }

    public ValidacionException(string mensaje, Exception innerException)
    {
        
    }
}
