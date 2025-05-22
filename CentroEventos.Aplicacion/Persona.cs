using System;
using System.Data.Common;
using System.Dynamic;


namespace CentroEventos.Aplicacion;


public class Persona {
    private int _id;
    private string? _dni;
    private string? _nombre;
    private string? _apellido;
    private string? _email;//public Email {get;set;}--->pasa a ser una propiedad 
    private string? _telefono;


    public Persona(int id,string? dni ,string? ape,string? nom, string? email , string? tel)

    {
        this._id = id;
        this._dni = dni; ///consulta a IrepositorioPersona
        this._nombre = nom;
        this._apellido =ape;
        this._email = email; 
        this._telefono = tel;
    }
    public int Id{
        get { return _id; }
    }
    public string? Dni{
        get{return _dni;}
        set{_dni = value;}
    }
    public string? Nombre{
        get{return _nombre;}
        set{ _nombre = value;}
    }
    public string? Apellido{
        get{return _apellido;}
        set{_apellido = value;}
    }
    public string? Email{
        get { return this._email;}
        set{_email = value;}
    }
    public string? Telefono{
        get{ return  _telefono;}
        set{}
    }
    public override string ToString(){
        string aux="";
        aux+= $"Persona: {this._id} , dni: {this._dni} , nombre: {this._nombre} , apellido: {this._apellido} , email: {this._email} , telefono: {this._telefono}";
        return aux;
    }

}