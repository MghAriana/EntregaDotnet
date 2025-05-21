using System;

namespace CentroEventos.Aplicacion;

public class PersonaModificacionesUseCase(IRepositorioPersona repo, PersonaValidador validador)
{
    public void Ejecutar(int id)
    {
        int opcion;
        Console.WriteLine("ingrese una opcion. 1: dni\n 2:nombre\n 3:apelllido\n 4:email\n 5: telefono");
        opcion = int.Parse(Console.ReadLine() ?? "");
        repo.modificarPersona(id, validador);
    }
}
