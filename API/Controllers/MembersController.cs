using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")] // Nos genera un localhost:5001/api/members
[ApiController]
public class MembersController(AppDbContext context) : ControllerBase
{
    [HttpGet] // api/members
    public ActionResult<IReadOnlyList<AppUser>> GetMembers()
    {
        //Hara un select de los usuarios
        var members = context.Users.ToList();
        return members;
    }

    //Se ejecuta este en lugar del de arriba cuando se pasa un id
    [HttpGet("{id}")] // Nos genera un localhost:5001/api/members/bob-id
    public ActionResult<AppUser> GetMember(string id)
    {
        var member = context.Users.Find(id);
        if (member == null) return NotFound();
        return member;
    }
}
