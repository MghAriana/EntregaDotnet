using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using CentroEventos.Aplicacion;
using Microsoft.Win32.SafeHandles;
namespace CentroEventos.Repositorios;

public class RepositorioPersonaTXT : IRepositorioPersona
{
    readonly string _nomArch = "Personas.txt";
    public void agregarPersona(Persona persona)
    {
        using var sw = new StreamWriter(_nomArch, true);
        try
        {
            string[] linea = { $"{persona.Id}" ,
                        $"{persona.Dni}",
                        $"{persona.Nombre}",
                        $"{persona.Apellido}",
                        $"{persona.Email}",
                        $"{persona.Telefono}"
        };
            sw.WriteLine(string.Join(",", linea));
            Console.WriteLine("Persona agregada: " + string.Join(",", linea));
        }
        catch (EntidadNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            sw.Dispose();//--------> para liberar recursos 
        }

    }
    public List<Persona> ListarPersonas()
    {
        List<Persona> lista = new List<Persona>();
        using var sr = new StreamReader(_nomArch);
        try
        {
            string? linea = sr.ReadLine();
            while (!string.IsNullOrEmpty(linea))
            {
                string[] campo = linea.Split(",");

                Persona persona = new Persona(int.Parse(campo[0]), campo[1], campo[2], campo[3], campo[4], campo[5] );
                lista.Add(persona);
                
                linea = sr.ReadLine();
            }
            return lista;
        }
        catch (Exception exc)
        {
            throw new Exception($"ocurrio un error al listar {exc.Message}");
        }
        finally
        {
            sr.Close(); // ó sr.Dispose();
        }
    }
    public void eliminarPersona(int id_persona) //intento hacer un borrado logico guardandome una lista con marca de borrado
    {
        List<Persona> listaP = this.ListarPersonas();
        int i = 0;
        bool personaEncontrada = false;
        while (i < listaP.Count && !personaEncontrada)
        {
            if (listaP[i].Id == id_persona)
            {
                listaP.RemoveAt(i);
                personaEncontrada = true;
                Console.WriteLine("Persona eliminada.");
            }
            i++;
        }
        using var sw = new StreamWriter(_nomArch, false);
        foreach (Persona persona in listaP)
        {
            string linea = $"{persona.Id},{persona.Dni},{persona.Nombre},{persona.Apellido},{persona.Email},{persona.Telefono}";
            sw.WriteLine(linea);
        }
        sw.Dispose();
    }

    public void modificarPersona(int id, PersonaValidador validador )
    {
       // Console.WriteLine("ingrese una opcion para modificar n 1:dni\n 2:nombre \n 3:apellido 4:email \n 5:telefono\n 6:salir");
        //int opcion = int.Parse(Console.ReadLine() ?? "");
        //var lista = new List<string>();
        List<Persona> listaPer = this.ListarPersonas();
        Persona? persona= listaPer.Find(per => per.Id == id);
        if (persona == null)
        {
            throw new Exception("no ingreso ninguna persona");
        }
                /*var campos = linea.Split(',');*/
        
        
        bool ok = true;
        while (ok) {
            Console.WriteLine("ingrese una opcion para modificar n 1:dni\n 2:nombre \n 3:apellido 4:email \n 5:telefono\n 6:salir");
            int opcion = int.Parse(Console.ReadLine() ?? "");
            switch (opcion)
            {   
                
                case 1:Console.WriteLine("ingrese el dato a modificar"); persona.Dni = Console.ReadLine(); break; //campos[2] = aux;
                case 2: Console.WriteLine("ingrese el dato a modificar");persona.Nombre = Console.ReadLine(); break;
                case 3: Console.WriteLine("ingrese el dato a modificar");persona.Apellido = Console.ReadLine(); break;
                case 4: Console.WriteLine("ingrese el dato a modificar");persona.Email = Console.ReadLine(); break;
                case 5: Console.WriteLine("ingrese el dato a modificar");persona.Telefono = Console.ReadLine(); break;
                case 6: ok = false; break;
                default: Console.WriteLine("opcionno valida"); break;
            }  
        }
         
        if (!validador.Validador(persona, out string mensajeError)) 
        {
            throw new Exception("no se pudo validar los datos");
        } 
        else 
        {
             this.eliminarPersona(id);
             this.agregarPersona(persona);
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
    public bool existeId(int id)
    {
        bool encontro = false;
        string? linea;
        using var sr = new StreamReader(_nomArch, true);
        while ((linea = sr.ReadLine()) != null && !encontro)
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
    public bool existeEmail(string mail)
    {
        bool encontro = false;
        List <Persona>  lista = this.ListarPersonas();
        Persona? persona = lista.Find(per => per.Email == mail);
        if (persona == null)
        {
            Console.WriteLine("no se encontro");
        }
        else
        {
            encontro = true;
        }
        /*string? linea;
        using var sr = new StreamReader(_nomArch, true);
        while ((linea = sr.ReadLine()) != null && !encontro)
        {
            string[] campo = linea.Split(',');

            if (campo[4] == mail)
            {
                encontro = true;
            }
        }
        sr.Dispose();*/

        return encontro;
    }
    public bool existePersona(int id)
    {
        bool ok = false;
        string? linea;
        using var sr = new StreamReader(_nomArch);
        
            while ((linea = sr.ReadLine()) != null && !ok)
        {
            string[] campo = linea.Split(',');
            if (int.Parse(campo[0]) == id)
            {
                ok = true;
            }
        }
        return ok;
        
    }
    
    //verificar si existe una persona asociada a un evento 
}
  

