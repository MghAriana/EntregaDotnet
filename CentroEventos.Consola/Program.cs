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
Console.WriteLine(System.Environment.Version);

//Configuro las dependencias:
IRepositorioPersona repoP = new RepositorioPersonaTXT();
IRepositorioEventoDeportivo repoED = new RepositorioEventoDeportivoTXT();
IRepositorioID repoID = new RepositorioIDTXT();

<<<<<<< HEAD
//Caso de uso: Alta de Evento 
var AgregarEvento = new EventoDeportivoAltaUseCase( new RepositorioEventoDeportivoTXT(), 
                                                    new EventoDeportivoValidador(repo));
Persona responsable = new Persona();
Persona participante1 = new Persona();
Persona participante2 = new Persona();
=======
// Caso de uso: Alta de Persona 
// Se crea una persona y se le asigna un id autogenerado por el repositorio
var AgregarPersona = new PersonaAltaUseCase(repoP, new PersonaValidador(repoP));
//Ejecuto el caso de uso:
AgregarPersona.Ejecutar( new Persona("38807484","Pisco","Sheila","sheilapisco@gmail.com","2976210323",repoID) );

// Caso de uso: Alta de Evento Deportivo 
// Se crea un evento y se le asigna un id autogenerado por el repositorio
var AgregarEvento = new EventoDeportivoAltaUseCase( repoED, new EventoDeportivoValidador(repoP));
>>>>>>> main

//Ejecuto el caso de uso:
AgregarEvento.Ejecutar( new EventoDeportivo(repoID,"Zumba Power-Up","Dejá de ser una pelotuda, vení a moverte!",
                        new DateTime(2025,5,15,15,5,6,325), 1, 25, 1) );

Console.WriteLine("funciona");