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

//Configuro las dependencias:
IRepositorioPersona repoP = new RepositorioPersonaTXT();
IRepositorioReserva repoR = new RepositorioReservaTXT();
IRepositorioEventoDeportivo repoE = new RepositorioEventoDeportivoTXT();
IRepositorioID repoID = new RepositorioIDTXT();
IRepositorioPersona rPersona = new RepositorioPersonaTXT();

// --------------------------> Casos de uso: Persona <--------------------------
var AgregarPersona = new PersonaAltaUseCase(repoP, new PersonaValidador(repoP));
var ListarPersonas = new ListarPersonasUseCase(repoP);
var EliminarPersona = new PersonaBajaUseCase();

//Alta:
//Hacer primero las validaciones dentro de AltaUseCase y luego generar el Id
AgregarPersona.Ejecutar( new Persona("45307494","Cerati","Gustavo","sodaEstereo@gmail.com","2216210323",repoID) );
AgregarPersona.Ejecutar( new Persona("42800880","Garcia","Charly","9noPiso@gmail.com","2216220330",repoID) );
AgregarPersona.Ejecutar( new Persona("39807682","Cantilo","Fabiana","amaneceEnLaRuta@gmail.com","2214210606",repoID) );
//Listar:
List <Persona> lista_personas = ListarPersonas.Ejecutar();
//Baja:
//Modificar:

// ----------------------> Casos de uso: EventoDeportivo <----------------------
var AgregarEvento = new EventoDeportivoAltaUseCase( repoE, new EventoDeportivoValidador(repoP));
var ListarEventos = new ListarEventoDeportivoUseCase(repoE);
var EliminarEvento = new EventoDeportivoBajaUseCase(repoE, repoR);
var ModificarEvento = new EventoDeportivoModificacionUseCase();
// Alta:
AgregarEvento.Ejecutar( new EventoDeportivo(repoID,"Zumba Power-Up","Dejá de ser una pelotuda y vení a moverte!",
                        new DateTime(2025,5,25,15,5,6,325), 1, 25, 3) );
AgregarEvento.Ejecutar( new EventoDeportivo(repoID,"Salto Olímpico","Sin comentarios",
                        new DateTime(2025,6,15,15,5,6,325), 2.5, 10, 2) );
// Listar:
List <EventoDeportivo> lista_eventos = ListarEventos.Ejecutar();
foreach(EventoDeportivo evento in lista_eventos)
{
    Console.WriteLine(evento.ToString());
}
// Baja:
EliminarEvento.Ejecutar(7); 
// Modificar:

// --------------------------> Casos de uso: Reserva <--------------------------