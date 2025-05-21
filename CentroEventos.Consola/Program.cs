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
(completo) de EventoDeportivo. 
Las operaciones de baja/eliminación recibirán el Id de la entidad a eliminar.
Además, debe implementarse:
ListarEventosConCupoDisponibleUseCase para obtener un listado de los eventos futuros donde aún existen cupos disponibles y
ListarAsistenciaAEventoUseCase para obtener la lista de todos los asistentes a un evento pasado.
*/

//-----------------------> Configuro las dependencias <-----------------------
//Interfaces:
IRepositorioPersona repoP = new RepositorioPersonaTXT();
IRepositorioEventoDeportivo repoE = new RepositorioEventoDeportivoTXT();
IRepositorioReserva repoR = new RepositorioReservaTXT(repoE, repoP);
IRepositorioID repoID = new RepositorioIDTXT();
IServicioAutorizacion autorizador = new ServicioDeAutorizacionProvisorio();
//Validadores:
PersonaValidador validadorP = new PersonaValidador(repoP);
EventoDeportivoValidador validadorE = new EventoDeportivoValidador(repoP);
ReservasValidador validadorR = new ReservasValidador(repoR, repoP);
//var autorizacion = new ServicioAutorizacionProvisorio();
//Casos de uso: Persona 
var AgregarPersona = new PersonaAltaUseCase(repoP, validadorP,repoID);
var ListarPersonas = new ListarPersonasUseCase(repoP);
var BorrarPersona = new PersonaBajaUseCase(repoP, repoR, repoE);
var ModificarPersona = new PersonaModificacionesUseCase(repoP, validadorP);
// Casos de uso: EventoDeportivo 
var AgregarEvento = new EventoDeportivoAltaUseCase( repoE, validadorE, repoID);
var ListarEventos = new ListarEventoDeportivoUseCase(repoE);
var BorrarEvento = new EventoDeportivoBajaUseCase(repoE, repoR);
var ModificarEvento = new EventoDeportivoModificacionUseCase(repoE, repoR, validadorE);
// Casos de uso: Reserva 
var AgregarReserva = new ReservasAltaUseCase(repoR, validadorR, repoID);
var ListarReserva = new ListarReservaUseCase();
var BorrarReserva = new ReservasBajaUseCase(repoR);
var ModificarReserva = new ReservasModificacionUseCase(repoR, validadorR);
// Casos de uso: Otros
var ListarEventosConCupoDisponible = new ListarEventosConCupoDisponibleUseCase(repoR);
var ListarAsistenciaAEventos = new ListarAsistenciaAEventosUseCase(repoR);

Console.Write("Ingrese número de usuario: "); int usuario = int.TryParse(Console.ReadLine(), out int us) ? us : 5;

bool terminar = false;
while (!terminar)
{
    Console.WriteLine("<------------------ Menú Principal ------------------>");
    Console.WriteLine(" 1. Agregar Persona                                   ");
    Console.WriteLine(" 2. Agregar Evento                                    ");
    Console.WriteLine(" 3. Agregar Reserva                                   ");
    Console.WriteLine(" 4. Borrar Persona                                    ");
    Console.WriteLine(" 5. Borrar Evento                                     ");
    Console.WriteLine(" 6. Borrar Reserva                                    ");
    Console.WriteLine(" 7. Modificar Persona                                 ");
    Console.WriteLine(" 8. Modificar Evento                                  ");
    Console.WriteLine(" 9. Modificar Reserva                                 ");
    Console.WriteLine(" 10. Listar Personas                                  ");
    Console.WriteLine(" 11. Listar Eventos                                   ");
    Console.WriteLine(" 12. Listar Reservas                                  ");
    Console.WriteLine(" 13. Listar Eventos Con Cupo Disponible               ");
    Console.WriteLine(" 14. Listar Asistencia a Eventos                      ");
    Console.WriteLine(" 15. Salir                                            ");
    Console.WriteLine("<---------------------------------------------------->");
    Console.Write(" Ingrese una opción: "); int opcion = int.TryParse(Console.ReadLine(), out int op) ? op : -1;

    switch (opcion)
    {
        case 1:
            if (autorizador.PoseeElPermiso(usuario,Permiso.UsuarioAlta)) AgregarPersona.Ejecutar();
            break;
        case 2:
            if (autorizador.PoseeElPermiso(usuario,Permiso.EventoAlta)) AgregarEvento.Ejecutar();
            break;
        case 3:
            if (autorizador.PoseeElPermiso(usuario,Permiso.ReservaAlta)) AgregarReserva.Ejecutar();
            break;
        case 4:
            if (autorizador.PoseeElPermiso(usuario, Permiso.UsuarioBaja))
            {
                Console.Write("Ingrese id de la persona que desea borrar: ");
                int id_persona_b = int.TryParse(Console.ReadLine(), out int id_p) ? id_p : -1;
                if (id_persona_b != -1) BorrarPersona.Ejecutar(id_persona_b);
            }
            break;
        case 5:
            if (autorizador.PoseeElPermiso(usuario, Permiso.EventoBaja))
            {
                Console.Write("Ingrese id del evento que desea borrar: ");
                int id_evento_b = int.TryParse(Console.ReadLine(), out int id_e) ? id_e : -1;
                if (id_evento_b != -1) BorrarEvento.Ejecutar(id_evento_b);
            }
            break;
        case 6:
            if (autorizador.PoseeElPermiso(usuario, Permiso.ReservaBaja))
            {
                Console.Write("Ingrese id de la reserva que desea borrar: ");
                int id_reserva_b = int.TryParse(Console.ReadLine(), out int id_r) ? id_r : -1;
                if (id_reserva_b != -1) BorrarReserva.Ejecutar(id_reserva_b);
            }
            break;
        case 7:
            if (autorizador.PoseeElPermiso(usuario, Permiso.UsuarioModificacion))
            {
                Console.Write("Ingrese id de la persona que desea modificar: ");
                int id_persona_m = int.TryParse(Console.ReadLine(), out int id_pm) ? id_pm : -1;
                if (id_persona_m != -1) ModificarPersona.Ejecutar(id_persona_m);
            }
            break;
        case 8:
            if (autorizador.PoseeElPermiso(usuario, Permiso.EventoModificacion))
            {
                Console.Write("Ingrese id del evento que desea modificar: ");
                int id_evento_m = int.TryParse(Console.ReadLine(), out int id_em) ? id_em : -1;
                if (id_evento_m != -1) ModificarEvento.Ejecutar(id_evento_m);
            }
            break;
        case 9:
            if (autorizador.PoseeElPermiso(usuario, Permiso.ReservaModificacion))
            {
                Console.Write("Ingrese id de la reserva que desea modificar: ");
                int id_reserva_m = int.TryParse(Console.ReadLine(), out int id_rm) ? id_rm : -1;
                if (id_reserva_m != -1) ModificarReserva.Ejecutar(id_reserva_m);
            }
            break;
        case 10:
            List<Persona> lista_personas = ListarPersonas.Ejecutar();
            foreach (Persona persona in lista_personas) Console.WriteLine(persona.ToString());
            break;
        case 11:
            List<EventoDeportivo> lista_eventos = ListarEventos.Ejecutar();
            foreach(EventoDeportivo evento in lista_eventos) Console.WriteLine(evento.ToString());
            break;
        case 12:
            List<Reserva> lista_reservas = ListarReserva.Ejecutar();
            foreach (Reserva reserva in lista_reservas) Console.WriteLine(reserva.ToString());
            break;
        case 13:
            List<EventoDeportivo> lista_eventos_con_cupo = ListarEventosConCupoDisponible.Ejecutar();
            foreach(EventoDeportivo evento in lista_eventos_con_cupo) Console.WriteLine(evento.ToString());
            break;
        case 14:
            Console.Write("Ingrese id del evento: ");
            int id_evento = int.TryParse(Console.ReadLine(), out int id) ? id: -1;
            if (id_evento != -1)
            {
                List<Persona> lista_asistencia = ListarAsistenciaAEventos.Ejecutar(1);
                foreach (Persona persona in lista_asistencia) Console.WriteLine(persona.ToString());
            }
            break;
        case 15:
            terminar = true; 
            break;
        default:
            Console.WriteLine(" La opción ingresada no es válida. Ingrese un número entre 1 y 15.");
            break;
    }
}


