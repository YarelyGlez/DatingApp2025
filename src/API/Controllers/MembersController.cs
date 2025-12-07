using API.Entities;
using API.Interfaces;
using API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class MembersController(IMembersRepository membersRepository) : BaseApiController
{
    [HttpGet] // api/members
    public async Task<ActionResult<IReadOnlyList<Member>>> GetMembers()
    {
        return Ok(await membersRepository.GetMembersAsync());
    }

    //Se ejecuta este en lugar del de arriba cuando se pasa un id
    [HttpGet("{id}")] // Nos genera un localhost:5001/api/members/bob-id
    public async Task<ActionResult<Member>> GetMember(string id)
    {
        var member = await membersRepository.GetMemberAsync(id);

        if (member == null) return NotFound();

        return member.ToResponse();
    }

    [HttpGet("{id}/photos")]
    public async Task<ActionResult<IReadOnlyList<Photo>>> GetPhotos(string id)
    {
        return Ok(await membersRepository.GetPhotosAsync(id));
    }
}
