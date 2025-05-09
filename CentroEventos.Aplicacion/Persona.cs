using System;
using System.Data.Common;
using System.Dynamic;


namespace CentroEventos.Aplicacion;

public class Persona {
    private int _id;
    private string? _dni;
    private string? _nombre;
    private string? _apellido;
    private string? _email;
    private string? _telefono;

    public Persona(int id, string dni ,string ape,string nom, string email , string tel)
    {
        _id= id;
        this._dni = dni; ///consulta a IrepositorioPersona
        this._nombre = nom;
        this._apellido =ape;
        this._email = email; //consulta  a Irepositorio
        this._telefono = tel;
    }

    public int Id{
        get { return _id; }
        set { _id = value; }
    }
    public string? Dni{
        get{return _dni;}
        set{_dni = value;}
    }
    public string? Nombre{
        get{return _email;}
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

    public string toString(){
        string aux="";
        aux+= $"Persona: {this._id} , \ndni: {this._dni} , \nnombre {this._nombre} , \napellido : {this._apellido} \nemail: {this._email} \ntelefono: {this._telefono}";
        return aux;
    }

}