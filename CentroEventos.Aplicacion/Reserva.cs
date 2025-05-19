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
        private Estado? _EstadoAsistencia;

        public Reserva()
        {
        }

        public Reserva (int id, int? idpersona, int? eventoid, DateTime? Fecha, Estado estado){
            this._id = id++;
            this._Persona_id = idpersona;
            this._EventoDeportivoid = eventoid;
            this._FechaAltaReserva = Fecha;
            this._EstadoAsistencia = estado;
        }
        public int Id{
            get { return _id; }
            set { _id = value; }
        }
        public int? Idpersona
        {
            get { return _Persona_id; }
            set { _Persona_id = value; }
        }
        public int? IdEven_Dep
        {
            get { return _EventoDeportivoid; }
            set { _EventoDeportivoid = value; }
        }
        public DateTime? Fecha
        {
            get { return _FechaAltaReserva; }
            set { _FechaAltaReserva = value; }
        }
        public Estado? EstadoAsistencia
        {
            get { return _EstadoAsistencia; }
            set { _EstadoAsistencia = (Estado)value; }
        }
        public string ToString()
        {
            string aux = "";
            aux += $"Reserva: {this._id} , \nPersona ID: {this._Persona_id} , \nEvento Deportivo ID: {this._EventoDeportivoid} , \nFecha de Alta : {this._FechaAltaReserva} \nEstado de Asistencia: {this._EstadoAsistencia}";
            return aux;
        }
        
        //Agregar propiedades para poder acceder a los atributos privados.
    }
}