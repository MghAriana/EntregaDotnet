using System;
using CentroEventos.Aplicacion;
namespace CentroEventos.Repositorios;

public class RepositorioPersonaTXT : IRepositorioPersona 
{
    readonly string _nomArch = "Personas.txt";
    public void agregarPersona(Persona persona)
    {
        using var sw = new StreamWriter(_nomArch, true);
        string[] linea= { $"{persona.Id}" , 
                        $"{persona.Dni}",
                        $"{persona.Nombre}",
                        $"{persona.Apellido}",
                        $"{persona.Email}",
                        $"{persona.Telefono}"
        };
        sw.WriteLine(string.Join(",", linea));
        sw.Dispose();//--------> para liberar recursos 
       
    }
    public List<Persona> ListarPersonas(){
        List<Persona> lista = new List<Persona>();
        
        return lista;
    }
    public bool existeDni(string dni){
        return false;
    }
}
