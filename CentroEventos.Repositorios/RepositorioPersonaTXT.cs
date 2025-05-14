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
        Console.WriteLine("Persona agregada: " + string.Join(",", linea));
        sw.Dispose();//--------> para liberar recursos 
       
    }
    public List<Persona> ListarPersonas(){
        List<Persona> lista = new List<Persona>();
        
        return lista;
    }
    public bool existeDni(string dni){
        return false;
    }

    public bool existeID(int id)
    {
        bool existe = false;
        Console.WriteLine("Buscando id " + id + " en el archivo " + _nomArch);
        using var sr = new StreamReader(_nomArch, true);
        string? linea = sr.ReadLine();
        Console.WriteLine("Leyendo linea: " + linea);
        while(linea != null && !existe)
        {   
            
            string[] campo = linea.Split(','); 
            // campos = [ Id, DNI, Nombre, Apellido, Email, Telefono ]
            if(int.Parse(campo[0]) == id) {
                existe = true;
            }   
            linea = sr.ReadLine(); // Leo la siguiente línea
        }
        sr.Dispose();
        return existe;
    }
}
