using System;
using CentroEventos.Aplicacion;
using Microsoft.Win32.SafeHandles;
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
    public List<Persona> ListarPersonas() {
        List<Persona> lista = new List<Persona>();
        using var sr = new StreamReader(_nomArch);
        bool encontre = false;
        while (!sr.EndOfStream && !encontre)
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
                encontre = true;
            }
            else
            {
                lista.Add(persona);
            }
        }
        return lista;
        }
    public void eliminarPersona(int id) //intento hacer un borrado logico guardandome una lista con marca de borrado
    {   
        bool personaEncontrada = false;
        using var sr = new StreamReader(_nomArch);
        using var sw = new StreamWriter(_nomArch);
        try
        {


            var lista = new List<string>();
            var listaMarca = new List<string>(); //---------> guardo en la lista los que tienen marca de borrado por si se necesita un historial con todos 
            string? linea;
            int idAct;
            while (!sr.EndOfStream && ((linea = sr.ReadLine()) != null))
            {
                var campo = linea.Split(',');
                idAct = int.Parse(campo[0]); //----------> Parse para convertir el string en un int 
                if (idAct == id)
                {
                    personaEncontrada = true;
                    campo[0] = "x";
                    listaMarca.Add($"{campo[0]},{campo[1]},{campo[2]},{campo[3]},{campo[4]},{campo[5]} , X "); //----------> uso X como marca de borrado
                }
                else
                {
                    lista.Add(linea);
                }
                foreach (var act in lista)
                {
                    sw.WriteLine(act);
                }
            }
            if (!personaEncontrada)
            {
                throw new Exception("no se encontro a la persona");
            }

        }
        catch (Exception ex)
        {
            throw new Exception($"Error al dar de baja a la persona: {ex.Message}", ex);

        }
        finally
        {
            sr.Dispose();
            sw.Dispose();
        }      
    }

    public bool existeDni(string dni)
    {
        bool encontro = false;
        string? linea; //metodo de StreamReader por lineas
        using var sr = new StreamReader(_nomArch, true);
        while ((linea = sr.ReadLine()) != null && !encontro)
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
  

