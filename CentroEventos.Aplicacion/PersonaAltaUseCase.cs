using System;

namespace CentroEventos.Aplicacion;

public class PersonaAltaUseCase(IRepositorioPersona repopersona , PersonaValidador validador)
{
    public void Ejecutar(Persona persona)
    {
        if (!validador.Validador(persona, out string mensajeError))
        {
            throw new Exception(mensajeError);
        }
        repopersona.agregarPersona(persona);
    }
    

}
