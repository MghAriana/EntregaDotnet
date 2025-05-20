using System;

namespace CentroEventos.Aplicacion;

public class PersonaModificacionesUseCase(IRepositorioPersona repo, PersonaValidador validador)
{
    public void Ejecutar(Persona per)
    {
        int opcion;
        Console.WriteLine("ingrese una opcion. 1: dni\n 2:nombre\n 3:apelllido\n 4:email\n 5: telefono");
        opcion = int.Parse(Console.ReadLine() ?? "");
        if (!validador.Validador(per, out string mensajeError))
        {
            throw new Exception(mensajeError);
        }
        repo.modificarPersona(per,opcion);
    }
}
