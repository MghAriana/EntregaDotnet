using System;

namespace CentroEventos.Aplicacion;

public interface IRepositorioReserva
{
    void AgregarReserva(Reserva unareserva);
    void RealizarBaja (int idReserva);
    public bool ExistePersona(int IdResponsable);
    public bool ExisteCupo(int idReserva);
    public bool existeReservaEvento(int idEvento);
    public List<EventoDeportivo> ListarEventosConCupo();
}
