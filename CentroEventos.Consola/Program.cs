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
IRepositorioEventoDeportivo repo = new RepositorioEventoDeportivoTXT();

//Caso de uso: Alta de Evento 
var AgregarEvento = new EventoDeportivoAltaUseCase( new RepositorioEventoDeportivoTXT(), 
                                                    new EventoDeportivoValidador(repo));

//Ejecuto el caso de uso:
AgregarEvento.Ejecutar( new EventoDeportivo() {Id = 1, Nombre = "Zumba Power-Up", 
                        Descripcion = "Dejá de ser una pelotuda, vení a moverte!", 
                        FechaHoraInicio = System.DateTime.Now, DuracionHoras = 1, 
                        CupoMaximo = 25, ResponsableId = 1 });
