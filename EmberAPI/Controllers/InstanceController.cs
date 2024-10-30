using AutoMapper;
using EmberAPI.Dtos;
using EmberAPI.Models;
using EmberAPI.Repositories;
using EmberCloudServices;
using Microsoft.AspNetCore.Mvc;

namespace EmberAPI.Controllers;
[Route("EmberAPI/[controller]")]
[ApiController]

public class InstanceController : Controller
{
    private readonly IInstanceRepository _repository;
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    private SqlInstance _sqlInstance = new SqlInstance();

    public InstanceController(IInstanceRepository repository, IMapper mapper)
    {
        this._repository = repository;
        _mapper = mapper;
    }

    [HttpGet("ListInstances")]
    public async Task<ActionResult<IEnumerable<InstanceDto>>> GetInstances()
    {
        var clusters = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<InstanceDto>>(clusters));
    }

    [HttpGet("GetInstanceByName/{name}")]
    public async Task<ActionResult<InstanceDto>> GetInstanceByName(string name)
    {
        var instance = await _repository.GetAsync(x => x.InstanceName == name);
        return Ok(_mapper.Map<InstanceDto>(instance));
    }
    [HttpGet("GetAllInstancesByClientID/{clientId}")]
    public async Task<ActionResult<IEnumerable<InstanceDto>>> GetInstanceByClient(int clientId)
    {
        var instances = await _repository.GetAllInstancesByClientIDAsync(clientId);
        if (!instances.Any()) return NotFound();
        
        return Ok(_mapper.Map<IEnumerable<InstanceDto>>(instances));
            
    }

    [HttpPost("CreateInstance")]

    public async Task<ActionResult<InstanceDto>> CreateInstance(string nombre, string password, [FromBody] InstanceDto instanceDto)
    {
        var client = await _clientRepository.GetAsync(x => x.ClientID == int.MaxValue);
        
        if(_sqlInstance.SqlInstanceCreate(nombre, password)) 
           
            return Ok();
        else
            return BadRequest();
    }
}