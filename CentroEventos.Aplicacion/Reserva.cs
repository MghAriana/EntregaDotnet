using System;

namespace CentroEventos.Aplicacion
{
    public enum Estado
    {
        Pendiente,
        Presente,
        Ausente
    }
    public class Reserva
    {
        
        private int _id; //gestionado por el repositorio
        private int? _Persona_id;
        private int? _EventoDeportivoid;
        private DateTime? _FechaAltaReserva;
        private Estado _EstadoAsistencia;

        public Reserva (int id, int? idpersona, int? eventoid, DateTime? Fecha, Estado estado){
            this._id = id++;
            this._Persona_id = idpersona;
            this._EventoDeportivoid = eventoid;
            this._FechaAltaReserva = Fecha;
            this._EstadoAsistencia = estado;
        }
        public override string ToString(){
            string aux="";
            aux+= $"Reserva: {this._id} , \nPersona ID: {this._Persona_id} , \nEvento Deportivo ID: {this._EventoDeportivoid} , \nFecha de Alta : {this._FechaAltaReserva} \nEstado de Asistencia: {this._EstadoAsistencia}";
            return aux;
        }
        
        //Agregar propiedades para poder acceder a los atributos privados.
    }
}