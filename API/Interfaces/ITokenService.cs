using System;
using API.Entities;

namespace API.Interfaces;

public interface ITokenService
{
    //Siempre las interfaces empiezan con I
    //Interfaces contiene las firmas de los metodos que vamos a implementar
    //No es una interfaz de usuario, es una interfaz de programacion
    public string CreateToken(AppUser user);
}
