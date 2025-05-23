using System;

namespace CentroEventos.Aplicacion;

public class PersonaModificacionesUseCase(IRepositorioPersona repo, PersonaValidador validador)
{
    public void Ejecutar(int id)
    {
        repo.modificarPersona(id, validador);
    }
}
