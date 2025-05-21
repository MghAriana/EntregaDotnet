using System;
using System.Data.Common;

namespace CentroEventos.Aplicacion;

public class ReservasModificacionUseCase (IRepositorioReserva repo, ReservasValidador validador)
{
    private void ModificarUnaReserva(Reserva r)
    {
        Reserva datos = cargarReservamodificada();
        r.Idpersona = datos.Idpersona;
        r.IdEven_Dep = datos.IdEven_Dep;
        r.Fecha = DateTime.Now;
        r.EstadoAsistencia = datos.EstadoAsistencia;
    }
    private Reserva cargarReservamodificada()
    {
        Console.WriteLine("Ingrese id de la persona: "); int idpersona = int.TryParse(Console.ReadLine(), out int id_p) ? id_p : -1 ;
        Console.WriteLine("Ingrese id del evento: "); int idevento = int.TryParse(Console.ReadLine(),out int id_e) ? id_e : -1;
        Console.WriteLine("Ingrese nuevo estado: (0 = Pendiente, 1 = Presente, 2 = Ausente)");
        bool estadoOk = int.TryParse(Console.ReadLine(), out int estNum);
        Estado estadoNuevo = estadoOk && Enum.IsDefined(typeof(Estado), estNum) ? (Estado)estNum : Estado.Pendiente;
        return new Reserva
        {
            Idpersona = id_p,
            IdEven_Dep = id_e,
            EstadoAsistencia = estadoNuevo
        };  
    }

    public void Ejecutar(int idReserva)
    {
        if (!repo.existeLaReserva(idReserva))
        {
            throw new Exception("No existe la reserva");
        }
        List<Reserva> lista = repo.ListarReserva();
        Reserva? reser = lista.Find(r => r.Id == idReserva);
        ModificarUnaReserva(reser);
        if (!validador.Validar(reser, out string error))
        {
            throw new Exception("Hay un error en la modificacion: " + error);
        }
        repo.Modificar(reser);
    }
}
