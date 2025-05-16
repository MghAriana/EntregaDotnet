using System;

namespace CentroEventos.Aplicacion;

public interface IRepositorioPersona
{
    public void agregarPersona(Persona Persona);

    public List<Persona> ListarPersonas();

    public bool existeDni(string dni);
    public bool existeEmail(string email);
    public bool existeId(int id);


}
