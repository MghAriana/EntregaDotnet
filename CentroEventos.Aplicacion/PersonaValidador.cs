using System;

namespace CentroEventos.Aplicacion;

public class PersonaValidador(IRepositorioPersona ipersona){
    
    public bool Validador(Persona Persona, out string mensajeError){
        mensajeError ="";
        
        if (string.IsNullOrWhiteSpace(Persona.Nombre))
        {
            throw new ValidacionException(mensajeError); // mensajeError = "debe proporcionar un nombre valido";
        }
        if (string.IsNullOrWhiteSpace(Persona.Apellido))
        {
            throw new ValidacionException(mensajeError);
        }
        if (string.IsNullOrWhiteSpace(Persona.Email))
        {
            throw new ValidacionException(mensajeError);
        }else
        {
            if (ipersona.existeEmail(Persona.Email))
            {
                throw new DuplicadoException(mensajeError) ;
            }
        }




        if (string.IsNullOrWhiteSpace(Persona.Dni))
        {
            throw new ValidacionException(mensajeError);

        }
        else
        {
            if (ipersona.existeDni(Persona.Dni))
            {
                throw new DuplicadoException(mensajeError) ;
            }
           /* if (!Persona.Dni.All(char.IsDigit))
            {
                mensajeError += "El DNI solo debe contener caracteres numéricos\n";
            }*/

        }
       
        return (mensajeError == "");
        
        
    }

}