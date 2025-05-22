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
    //readonly string borrado = "borrado.txt";
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
    public void eliminarPersona(int id_persona) 
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
    /*
    public void eliminarPersona(int id) //intento hacer un borrado logico guardandome una lista con marca de borrado
    {
        bool personaEncontrada = false;
        var lista = new List<string>();
        var listaMarca = new List<string>(); //---------> guardo en la lista los que tienen marca de borrado por si se necesita un historial con todos 
        try
        {
            using var sr = new StreamReader(_nomArch, true);
            string? linea;
            int idAct;
            while ((linea = sr.ReadLine()) != null && !personaEncontrada)
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
            }
            using var sw = new StreamWriter(_nomArch,false);//---->false para sobreescribir
            foreach (var act in lista)
            {
                sw.WriteLine(act);
            }
            using var sw2 = new StreamWriter(borrado,true); //-------> archivo historico con los borrados
            foreach (var act in listaMarca)
            {
                sw2.WriteLine(act);
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
    }
    */
    public void modificarPersona(int id, PersonaValidador validador)
    {
        Console.WriteLine("ingrese una opcion para modificar n 1:dni\n 2:nombre \n 3:apellido 4:email \n 5:telefono");
        int opcion = int.Parse(Console.ReadLine() ?? "");
        //var lista = new List<string>();
        List<Persona> listaPer = this.ListarPersonas();
        Persona? persona= listaPer.Find(per => per.Id == id);

        if (persona == null)
        {
            throw new Exception("no ingreso ninguna persona");
        }
                /*var campos = linea.Split(',');*/

                
        Console.WriteLine("ingrese el dato a modificar");
        string aux = Console.ReadLine() ?? "";
        switch (opcion)
        {
        case 1: persona.Dni = aux; break; //campos[2] = aux;
        case 2: persona.Nombre = aux; break;
        case 3: persona.Apellido = aux; break;
        case 4: persona.Email = aux; break;
        case 5: persona.Telefono = aux; break;
        default: throw new Exception("opcionno valida");
        }
                
        if (!validador.Validador(persona, out string mensajeError)) 
        {
            throw new Exception("no se pudo validar los datos");
        } else {
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
        string? linea;
        using var sr = new StreamReader(_nomArch, true);
        while ((linea = sr.ReadLine()) != null && !encontro)
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
  

