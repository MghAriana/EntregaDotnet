using System;
using CentroEventos.Aplicacion;

namespace CentroEventos.Repositorios;

public class RepositorioIDTXT : IRepositorioID
{
    readonly string _archivo = "ultimos_Ids_generados.txt"; // Contiene una sola línea: "id_persona, id_evento, id_reserva"
    public int GenerarId(string nom_clase)
    {
        Console.WriteLine("Generando id_" + nom_clase + " ... ");

        using var sr = new StreamReader(_archivo, true); 
        int id = 0;
        string? linea = sr.ReadLine(); //Leo la línea    
        sr.Dispose();
        Console.WriteLine("Linea leida: " + linea);
        
        if(string.IsNullOrEmpty(linea))
        {
            Console.WriteLine("El archivo está vacío. Se generará el id 1.");
            linea = "0,0,0"; // Si el archivo está vacío, inicializo los ids en 0
        }
        string[] ids = linea.Split(','); //Creo un vector de string [ "id_persona" | "id_evento" | "id_reserva" ]

        if(nom_clase == "Persona")
        {
            id = int.Parse(ids[0]) + 1; // Genero nuevo id para la clase Persona
            ids[0] = id.ToString(); // Modifico el valor de id_persona.  
        }
        else if(nom_clase == "EventoDeportivo")
        {
            id = int.Parse(ids[1]) + 1; // Genero nuevo id para la clase EventoDeportivo
            ids[1] = id.ToString(); // Modifico el valor de id_evento
        }
        else if(nom_clase == "Reserva")
        {
            id = int.Parse(ids[2]) + 1; // Genero nuevo id para la clase Reserva
            ids[2] = id.ToString(); //Modifico el valor de id_reserva
        }

        linea = string.Join(",", ids); // Vuelvo a generar el string con el id actualizado.
        Console.WriteLine("Linea actual: " + linea);
        using var sw = new StreamWriter(_archivo, false); //false para sobreescribir el archivo.
        sw.WriteLine(linea); // Sobrescribo el archivo con la nueva linea.
        sw.Dispose();

        Console.WriteLine("id_" + nom_clase + " generado con éxito!");
        return id; 
    }
}
