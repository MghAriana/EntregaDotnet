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
    public List<Persona> ListarPersonas() {
        List<Persona> lista = new List<Persona>();
        using var sr = new StreamReader(_nomArch);
        while (!sr.EndOfStream)
        {
            var persona = new Persona();
            persona.Id = int.Parse(sr.ReadLine() ?? "");
            persona.Dni = sr.ReadLine() ?? "";
            persona.Email = sr.ReadLine() ?? "";
            persona.Nombre = sr.ReadLine();
            persona.Apellido = sr.ReadLine();
            persona.Telefono = sr.ReadLine();
            if (existeId(persona.Id) && existeDni(persona.Dni) && existeEmail(persona.Email))
            {
                break;
            }
            else
            {
                lista.Add(persona);
            }
        }
        return lista;
        }

    public bool existeDni(string dni){
        bool encontro = false;
        string? linea; //metodo de StreamReader por lineas
        using var sr = new StreamReader(_nomArch, true);
        while((linea = sr.ReadLine()) != null && !encontro) 
        {
            string[] campo = linea.Split(',');

            if (campo[1] == dni)
            {
                encontro = true;
            }
        }
        sr.Dispose();
        return encontro;
    }
    public bool existeId(int id){
        bool encontro = false;
        string? linea;
        using var sr = new StreamReader(_nomArch, true);
        while((linea = sr.ReadLine()) != null && !encontro) 
        {
            string[] campo = linea.Split(',');

            if (int.Parse(campo[0]) == id)
            {
                encontro = true;
            }
        }
        sr.Dispose();
        return encontro;
    }
    public bool existeEmail(string mail){
        bool encontro = false;
        string? linea;
        using var sr = new StreamReader(_nomArch, true);
        while((linea = sr.ReadLine()) != null && !encontro) 
        {
            string[] campo = linea.Split(',');

            if (campo[4] == mail)
            {
                encontro = true;
            }
        }
        sr.Dispose();
        return encontro;
    }
    }
  

