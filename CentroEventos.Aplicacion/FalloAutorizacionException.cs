using System;

namespace CentroEventos.Aplicacion;

public class FalloAutorizacionException : Exception
{
    public FalloAutorizacionException()
            : base("No tiene autorizacion para realizar esta accion") { } ///---------------> base es para heredar de la superclase

    public FalloAutorizacionException(string mensaje)
        : base(mensaje) { }

    public FalloAutorizacionException(string mensaje, Exception innerException) //----> innerException para conservar información de la excepcion anterior  
        : base(mensaje, innerException) { }
            
    /* otra forma de hacerlo es con un try/ catch 
        try{
            validarque el usuario tiene permiso
        }catch{
            throw new Exception("no tiene autorizacion")
        }
    */
    
}
