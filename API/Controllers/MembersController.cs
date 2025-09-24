using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class MembersController(AppDbContext context) : BaseApiController
{
    [HttpGet] // api/members
    public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
    {
        //Hara un select de los usuarios, asíncrono, tiene que tener un await
        var members = await context.Users.ToListAsync();

        return members;
    }

    [AllowAnonymous]
    //Se ejecuta este en lugar del de arriba cuando se pasa un id
    [HttpGet("{id}")] // Nos genera un localhost:5001/api/members/bob-id
    public async Task<ActionResult<AppUser>> GetMember(string id)
    {
        var member = await context.Users.FindAsync(id);

        if (member == null) return NotFound();

        return member;
    }
}
