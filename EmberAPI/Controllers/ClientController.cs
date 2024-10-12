using AutoMapper;
using EmberAPI.Context;
using EmberAPI.Dtos;
using EmberAPI.Models;
using EmberAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmberAPI.Controllers;

[Route("EmberAPI/[controller]")]
[ApiController]
public class ClientController: Controller
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

    
    
    
    
    
}