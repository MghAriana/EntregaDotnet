using System.Reflection.PortableExecutable;
using CentroEventos.Aplicacion;
using CentroEventos.Repositorios;
/*Reglas de Negocio
● Un EventoDeportivo no puede tener más Reservas que su CupoMaximo.
No puede modificarse un EventoDeportivo cuya FechaHoraInicio haya expirado (es decir, no puede
modificarse un evento pasado).
● Al crear o modificar un EventoDeportivo, no puede establecerse la FechaHoraInicio con un valor
anterior al actual (es decir que sólo se permite si la fecha que va a registrarse es >= fecha actual).
● No puede eliminarse un EventoDeportivo si existen Reservas asociadas al mismo
(independientemente del estado de las reservas).
*/

/*Casos de Uso 
(en CentroEventos.Aplicacion)
Se deben implementar los casos de uso CRUD básicos para realizar Altas, Bajas, Modificaciones y Listado
(completo) de EventoDeportivo. Las operaciones de baja/eliminación recibirán el Id
de la entidad a eliminar.
Además, debe implementarse ListarEventosConCupoDisponibleUseCase
para obtener un listado de los eventos futuros donde aún existen cupos disponibles y
ListarAsistenciaAEventoUseCase para obtener la lista de todos los asistentes a un evento pasado.
*/

//-----------------------> Configuro las dependencias <-----------------------
//Interfaces:
IRepositorioPersona repoP = new RepositorioPersonaTXT();
IRepositorioEventoDeportivo repoE = new RepositorioEventoDeportivoTXT();
IRepositorioReserva repoR = new RepositorioReservaTXT(repoE);
IRepositorioID repoID = new RepositorioIDTXT();
IRepositorioPersona rPersona = new RepositorioPersonaTXT();
//Validadores:
PersonaValidador validadorP = new PersonaValidador(repoP);
EventoDeportivoValidador validadorE = new EventoDeportivoValidador(repoP);
ReservasValidador validadorR = new ReservasValidador(repoR, repoP, repoE);

// --------------------------> Casos de uso: Persona <--------------------------
var AgregarPersona = new PersonaAltaUseCase(repoP, validadorP,repoID);
var ListarPersonas = new ListarPersonasUseCase(repoP);
var EliminarPersona = new PersonaBajaUseCase();
var ModificarPersona = new PersonaModificacionesUseCase();

//Alta:
//Hacer primero las validaciones dentro de AltaUseCase y luego generar el Id
AgregarPersona.Ejecutar();
AgregarPersona.Ejecutar();
//Listar:
List <Persona> lista_personas = ListarPersonas.Ejecutar();
//Baja:
//Modificar:

// ----------------------> Casos de uso: EventoDeportivo <----------------------
var AgregarEvento = new EventoDeportivoAltaUseCase( repoE, validadorE, repoID);
var ListarEventos = new ListarEventoDeportivoUseCase(repoE);
var EliminarEvento = new EventoDeportivoBajaUseCase(repoE, repoR);
var ModificarEvento = new EventoDeportivoModificacionUseCase();
// Alta:
AgregarEvento.Ejecutar();
AgregarEvento.Ejecutar();
// Listar:
List <EventoDeportivo> lista_eventos = ListarEventos.Ejecutar();
foreach(EventoDeportivo evento in lista_eventos)
{
    Console.WriteLine(evento.ToString());
}
// Baja:
EliminarEvento.Ejecutar(2);
// Modificar:
ModificarEvento.Ejecutar();
// --------------------------> Casos de uso: Reserva <--------------------------
var AgregarReserva = new ReservasAltaUseCase(repoR, validadorR);
var ListarReserva = new ListarReservaUseCase();
var EliminarReserva = new ReservasBajaUseCase(repoR);
var ModificarReserva = new ReservasModificacionUseCase(repoR,validadorR);

AgregarReserva.Ejecutar();
AgregarReserva.Ejecutar();

List<Reserva> lista_reservas = ListarReserva.Ejecutar();
foreach (Reserva reserva in lista_reservas)
{
    Console.WriteLine(reserva.ToString());
}

EliminarReserva.Ejecutar(1);

ModificarReserva.Ejecutar();
