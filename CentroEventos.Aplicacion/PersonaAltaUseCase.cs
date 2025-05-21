using System;

namespace CentroEventos.Aplicacion;

public class PersonaAltaUseCase(IRepositorioPersona repopersona , PersonaValidador validador, IRepositorioID repoID)
{
    private Persona cargarPersona()
    {
        Console.WriteLine("Ingresar datos personales: ");
        Console.WriteLine("Dni: "); string dni = Console.ReadLine() ?? "";
        Console.WriteLine("Apellido: "); string ape = Console.ReadLine() ?? "";
        Console.WriteLine("Nombre: "); string nom = Console.ReadLine() ?? "";
        Console.WriteLine("Email: "); string email = Console.ReadLine() ?? "";
        Console.WriteLine("Telefo: "); string tel = Console.ReadLine() ?? "";
        return new Persona(repoID.GenerarId("Persona"), dni, ape, nom, email, tel);
    }
    public void Ejecutar()
    {
        Persona persona = this.cargarPersona();
        if (!validador.Validador(persona, out string mensajeError))
        {
            throw new Exception(mensajeError);
        }
        repopersona.agregarPersona(persona);
    }
    

}
