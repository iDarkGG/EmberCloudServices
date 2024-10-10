using AutoMapper;
using EmberAPI.Context;
using EmberAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmberAPI.Controllers;

[Route("client")]
[ApiController]
public class ClientController: Controller
{
    private readonly IMapper _mapper;
    private readonly MainContext _context;

    public ClientController(MainContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IEnumerable<Client> Get()
    {
        return _context.Clients.ToList();
    }
    
    
    
}