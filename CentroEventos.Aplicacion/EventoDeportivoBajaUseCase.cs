using System;

namespace CentroEventos.Aplicacion;

public class EventoDeportivoBajaUseCase(IRepositorioEventoDeportivo repo)
{
    public void Ejecutar(int id_evento)
    {
        repo.BajarEvento(id_evento);
    }
}
