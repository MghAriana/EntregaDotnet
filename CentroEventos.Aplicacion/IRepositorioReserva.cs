using System;

namespace CentroEventos.Aplicacion;

public interface IRepositorioReserva
{
    void AgregarReserva(Reserva unareserva);
    void RealizarBaja(int idReserva);
    void Modificar(Reserva unareserva);
    public bool existeReservaAsociadaAPersona(int idpersona);
    public bool ExisteResposable(int IdResponsable);
    public bool ExisteCupo(int idEvento);
    public bool existenReservasAsociadasAlEvento(int id_evento);
    public bool existeReservaRegistrada(int id_persona, int id_evento);
    public List<EventoDeportivo> ListarEventosConCupo();
    public List<Persona> ListarAsistencia(int idEvento);
    public List<Reserva> ListarReserva();
    public int ContarReservasSegunEvento(int id_evento);
}
