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
    /*
     * Por hacer:
     * necesito que el front end pase los siguientes datos:
     * -instancia actual
     * -Data center actual
     * - Cliente actual
     */
    [HttpPost("CreateInstance")]

    public async Task<ActionResult<InstanceDto>> CreateInstance(string nombre, string password, int currentUserId)
    {
        var instance = new InstanceDto
        {
            InstanceName = nombre,
            DatacenterID = "DC1",
            ClientId = currentUserId,
            CreationDate = DateOnly.FromDateTime(DateTime.UtcNow)
        };

        if (_sqlInstance.SqlInstanceCreate(nombre, password))
        {
            await _repository.Add(_mapper.Map<Instance>(instance));
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpDelete("DeleteInstance")]
    public async Task<ActionResult<InstanceDto>> DropInstance([FromBody] InstanceDto instanceDto)
    {
        if (_sqlInstance.SqlInstanceDrop(instanceDto.InstanceName))
        {
            await _repository.Add(_mapper.Map<Instance>(instanceDto));
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    // [HttpPut("UpdateInstance")]
    // public async Task<ActionResult<InstanceDto>> UpdateInstance([FromBody] InstanceDto instanceDto)
    // {
    //     
    // }

    // [HttpPost("AddUserToInstance")]
    // public async Task<ActionResult<InstanceDto>> AddUserToInstance([FromBody] CreatedUserDto userDto, int instanceId)
    // {
    //     var instance = await _repository.GetAsync(x => x.InstanceID == instanceId);
    //     return Ok(_mapper.Map<InstanceDto>(instance));
    // }
    //
}