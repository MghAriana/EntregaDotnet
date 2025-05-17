using System;
using CentroEventos.Aplicacion;

namespace CentroEventos.Repositorios;

public class RepositorioIDTXT : IRepositorioID
{
    private static readonly Dictionary<string, int> _Indices = new()
    {
        { "Persona", 0},
        { "EventoDeportivo", 1},
        { "Reserva", 2}
    };

    readonly string _archivo = "ultimos_Ids_generados.txt"; // Contiene una sola línea: "id_persona, id_evento, id_reserva"
    public int GenerarId(string nom_clase)
    {
        Console.WriteLine("Generando id_" + nom_clase + " ... ");
        var sr = new StreamReader(_archivo, true);
        string? linea = sr.ReadLine(); sr.Dispose();

        // Si el archivo está vacío, inicializo los ids en 0.
        if (string.IsNullOrEmpty(linea))
        {
            Console.WriteLine("El archivo está vacío. Se generará el id 1.");
            linea = "0,0,0";
        }
        _Indices.TryGetValue(nom_clase, out int pos);
        string[] ids = linea.Split(','); 
        int id = int.Parse(ids[pos]) + 1;

        // Actualizo el archivo con el último id generado.
        ids[pos] = id.ToString();
        linea = string.Join(",", ids); 
        using var sw = new StreamWriter(_archivo, false); 
        sw.WriteLine(linea); sw.Dispose();

        return id;
    }
}
