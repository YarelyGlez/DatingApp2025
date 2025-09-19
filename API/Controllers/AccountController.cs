using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(AppDbContext context) : BaseApiController
{
    //Inyectos y creamos metodos
    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> Register(RegisterRequest request) //Recibimos el request
    {
        if (await EmailExists(request.Email)) return BadRequest("Email is already in use"); //Si el email ya existe Retornamos un error

        //Using para asegurar que se elimine el objeto despues de usarlo
        using var hmac = new HMACSHA512(); //Algoritmo de encriptacion, algoritmos de llave de HASH

        var user = new AppUser
        {
            DisplayName = request.DisplayName,
            Email = request.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
            PasswordSalt = hmac.Key
        };

        context.Users.Add(user); //Magia de Entity Framework
        await context.SaveChangesAsync(); //Guardar cambios de manera asincrona

        return user; //Retornamos el usuario
    }

    //Para que no haya emails repetidos
    private async Task<bool> EmailExists(string email)
    {
        return await context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
    }

}
