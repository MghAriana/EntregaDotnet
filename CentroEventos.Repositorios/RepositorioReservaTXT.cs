using System;
namespace CentroEventos.Repositorios;

using System.Collections.Generic;
using CentroEventos.Aplicacion;

public class RepositorioReservaTXT : IRepositorioReserva
{
    public bool ExistenReservas(int idEvento)
    {
        bool existe = false;
        return existe;
    } 

    public List<EventoDeportivo> ListarEventosConCupo()
    {
        throw new NotImplementedException();
    }

}// Consultar como persistir el ID en particular si es correcto almacenar el valor del ultimo (ID) en un txt y asignarlo a la variable id de persona 
