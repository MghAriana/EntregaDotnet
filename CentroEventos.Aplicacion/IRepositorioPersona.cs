using System;

namespace CentroEventos.Aplicacion;

public interface IRepositorioPersona
{
    public void agregarPersona(Persona Persona);

    public List<Persona> ListarPersonas();
    public void eliminarPersona(int id);
    public void modificarPersona(int id, PersonaValidador validador);
    public bool existeDni(string dni);
    public bool existePersona(int id);
    public bool existeEmail(string email);
    public bool existeId(int id);


}
