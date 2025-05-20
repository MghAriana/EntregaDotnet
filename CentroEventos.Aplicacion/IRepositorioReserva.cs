using System;

namespace CentroEventos.Aplicacion;

public interface IRepositorioReserva
{
    void AgregarReserva(Reserva unareserva);
    void RealizarBaja(int idReserva);
    void Modificar(Reserva unareserva);
    public bool existeLaReserva(int idReserva);
    public bool ExisteResposable(int IdResponsable);
    public bool ExisteCupo(int idEvento);
    public bool existenReservas(int idEvento);
    public List<EventoDeportivo> ListarEventosConCupo();
    public int ContarReservasSegunEvento(int id_evento);
}
