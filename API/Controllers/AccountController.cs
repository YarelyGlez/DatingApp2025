using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;

namespace API.Controllers;

public class AccountController(AppDbContext context, ITokenService tokenService) : BaseApiController
{
    //Inyectos y creamos metodos
    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register(RegisterRequest request) //Recibimos el request
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

        return new UserResponse
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = tokenService.CreateToken(user)          
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserResponse>> Login(LoginRequest request)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Email == request.Email); //Buscar usuario por email

        if (user == null) return Unauthorized("Invalid email or password"); //Si no existe el usuario, retornamos error

        using var hmac = new HMACSHA512(user.PasswordSalt); //Usamos la misma llave para encriptar la contraseña que se guardo

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)); //Encriptamos la contraseña que nos dieron

        for (int i = 0; i < computedHash.Length; i++) //Comparamos los hashes
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid email or password"); //Si no son iguales, retornamos error
        }
        
        return new UserResponse
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = tokenService.CreateToken(user)          
        };
    }

    //Para que no haya emails repetidos
    private async Task<bool> EmailExists(string email)
    {
        return await context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
    }

}
