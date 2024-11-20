using AutoMapper;
using EmberAPI.Dtos;
using EmberAPI.Models;
using EmberAPI.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace EmberAPI.Controllers;

[Route("EmberAPI/[controller]")]
[ApiController]
public class ClientController : Controller
{
    private readonly IClientRepository _context;
    private readonly IMapper _mapper;

    public ClientController(IMapper mapper, IClientRepository repository)
    {
        _mapper = mapper;
        this._context = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetClients()
    {
        var lst = await _context.GetAllAsync();
        return Ok(_mapper.Map<List<ClientDto>>(lst));
    }

    [HttpGet("get-client-by-name/{name}")]
    public async Task<ActionResult<ClientDto>> GetClientByName(string? name)
    {
        if (name == null) return BadRequest();

        var user = await _context.GetAsync(x => x.ClientName == name);

        if (user is null) return NotFound();

        return Ok(_mapper.Map<ClientDto>(user));

    }
    

    [HttpPost("create-client")]
    public async Task<ActionResult> PostClient([FromBody] ClientPOSTDto client)
    {
        var searchClient = await _context.GetAsync(x => x.ClientName == client.ClientName);
        if (searchClient is not null) return BadRequest();

        if (client is null) return BadRequest();
        
        await _context.Add(_mapper.Map<Client>(client));
        return Ok();
    }

    [HttpPut("update-client/{id}")]
    public async Task<ActionResult> PutClient([FromBody] ClientDto client, int id)
    {
        if (id == 0) return BadRequest();

        if (client is null) return BadRequest();

        var usertofind = await _context.GetAsync(x => x.ClientID == id);
        if (usertofind is null) return BadRequest();

        usertofind = _mapper.Map<Client>(client);

        await _context.UpdateAsync(usertofind);
        return Ok();
    }

    [HttpPut("update-client-by-name/{name}")]
    public async Task<ActionResult> PutClientByName(string? name, [FromBody] ClientDto client)
    {
        if (name == null) return BadRequest();
        if (client is null) return BadRequest();

        var user = await _context.GetAsync(x => x.ClientName.ToUpper() == name.ToUpper());

        if (user is null) return BadRequest();

        user = _mapper.Map<Client>(client);

        await _context.UpdateAsync(user);

        return Ok();
    }


    [HttpPatch("patch-client/{name}")]
    public async Task<ActionResult> PatchClientByName(string name, [FromBody] JsonPatchDocument<ClientDto> patchDoc)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Client name is required.");

        if (patchDoc is null)
            return BadRequest("Patch document cannot be null.");

        var clientEntity = await _context.GetAsync(x => x.ClientName.ToUpper() == name.ToUpper());
        if (clientEntity is null)
            return NotFound($"Client with name '{name}' not found.");

        var clientDto = _mapper.Map<ClientDto>(clientEntity);

        patchDoc.ApplyTo(clientDto, ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _mapper.Map(clientDto, clientEntity);

        await _context.UpdateAsync(clientEntity);

        return Ok("Client successfully updated.");
    }

    [HttpDelete("delete-client/{name}")]
    public async Task<ActionResult> DeleteClient(string name)
    {
        if (name == null) return BadRequest();
        var user = await _context.GetAsync(x => x.ClientName.ToUpper() == name.ToUpper());

        if (user is null) return NotFound(ModelState);

        await _context.DeleteAsync(user);
        return Ok();
    }

    
}