using System;
using System.Data.Common;

namespace CentroEventos.Aplicacion;

public class ReservasModificacionUseCase (IRepositorioReserva repo, ReservasValidador validador)
{
    private void ModificarUnaReserva(Reserva r)
    {
        Console.WriteLine("Ingrese id de la persona: ");
        r.Idpersona = int.TryParse(Console.ReadLine(), out int id_p) ? id_p : -1 ;
        Console.WriteLine("Ingrese id del evento: ");
        r.IdEven_Dep = int.TryParse(Console.ReadLine(),out int id_e) ? id_e : -1;
        r.Fecha = DateTime.Now;
        Console.WriteLine("Ingrese nuevo estado: (0 = Pendiente, 1 = Presente, 2 = Ausente)");
        bool estadoOk = int.TryParse(Console.ReadLine(), out int estNum);
        Estado estadoNuevo = estadoOk && Enum.IsDefined(typeof(Estado), estNum) ? (Estado)estNum : Estado.Pendiente;
        r.EstadoAsistencia = estadoNuevo;
    }
    public void Ejecutar(int idReserva)
    {
        List<Reserva> lista = repo.ListarReserva();
        Reserva? reser = lista.Find(r => r.Id == idReserva);
        if (reser == null)
        {
            throw new Exception("No existe la reserva");
        }
        ModificarUnaReserva(reser);
        if (!validador.Validar(reser, out string error))
        {
            throw new Exception("Hay un error en la modificacion: " + error);
        }
        repo.Modificar(reser);
    }
}
