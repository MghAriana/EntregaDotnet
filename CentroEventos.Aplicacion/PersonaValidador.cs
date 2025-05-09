using System;

namespace CentroEventos.Aplicacion;

public class PersonaValidador(IRepositorioPersona ipersona){
    
    public bool Validador(Persona Persona, out string mensajeError){
        mensajeError ="";
        if(string.IsNullOrWhiteSpace(Persona.Nombre)){
            mensajeError = "debe proporcionar un nombre valido";
        }
        if (string.IsNullOrWhiteSpace(Persona.Apellido)){
            mensajeError = "debe proporcionar un apellido valido\n";
        }
        if( string.IsNullOrWhiteSpace(Persona.Email)){
            mensajeError= "el campo email no puede estar vacio\n";
        }
        
       if(string.IsNullOrWhiteSpace(Persona.Dni)){
            mensajeError = "el campo dni no puede estar vacio\n";
        }else{
            if(ipersona.existeDni(Persona.Dni)){
            mensajeError = "Ya existe una persona con el dni ingresado";
        }
        }
       
        return (mensajeError == "");
        
        
    }

}