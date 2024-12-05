using AutoMapper;
using EmberAPI.BackgroundServices;
using EmberAPI.Dtos;
using EmberAPI.Models;
using EmberAPI.Repositories;
using EmberCloudServices;
using EmberCloudServices.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace EmberAPI.Controllers;
[Route("EmberAPI/[controller]")]
[ApiController]

public class InstanceController : Controller
{
    private readonly IInstanceRepository _repository;
    private readonly ICreatedUserRepository _createdUserRepository;
    private readonly IMapper _mapper;
    private SqlInstance _sqlInstance = new SqlInstance();
    private StringHasher _hasher = new StringHasher();
    private UserPOSTService _userPostService;
    private DbSchema _dbSchema = new DbSchema();
    private DbTreeQuery _dbTreeQuery;
    
    public InstanceController(IInstanceRepository repository, IMapper mapper, UserPOSTService userPostService, ICreatedUserRepository createdUserRepository)
    {
        this._repository = repository;
        _mapper = mapper;
        _userPostService = userPostService;
        _createdUserRepository = createdUserRepository;
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

    [HttpGet("GetAllDatabases")]
    public async Task<IEnumerable<DbSchema>> GetAllDatabases(int InstanceId, string saPassword)
    {
        var instance = await _repository.GetAsync(x => x.InstanceID == InstanceId);
        _dbTreeQuery = new DbTreeQuery($"Server=localhost\\{instance.InstanceName.ToUpper()};Database=master;User Id=sa;Password={saPassword};TrustServerCertificate=True;");
        _dbTreeQuery.QueryAllDatabases();
        return await DbSchema.GetAllDbsAsync();
    }
    
    [HttpPost("create-instance")]

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
            return Ok("Success");
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpDelete("delete-instance")]
    public async Task<ActionResult<InstanceDto>> DropInstance(string instanceName)
    { 
        var instance = await _repository.GetAsync(x => x.InstanceName == instanceName);
        var result = await _createdUserRepository.GetAllUsersByInstanceIDAsync(instance.InstanceID);
        if(result.Any()) return BadRequest("Hay usuarios registrados en esta instancia, si desea borrarla, elimine los usuarios primero");
        await _repository.DeleteAsync(_mapper.Map<Instance>(instance));
        if (_sqlInstance.SqlInstanceDrop(instanceName))
        {
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

    [HttpPost("add-user-to-instance")]
    public async Task<ActionResult> AddUserToInstance([FromBody] CreatedUserPOSTDto userDto, int instanceId, string saPassword)
    {
        var instance = await _repository.GetAsync(x => x.InstanceID == instanceId);
        SqlUserManager _newUser = new SqlUserManager($"Server=localhost\\{instance.InstanceName.ToUpper()};Database=master;User Id=sa;Password={saPassword};TrustServerCertificate=True;");
        if (instance is null) return BadRequest("Instance not found");
        else
        {
            if (userDto is null) return BadRequest("Invalid User");
            else
            {
                if (_newUser.AddUserToSqlInstance(userDto.userNameHash, userDto.userPasswordHash))
                {
                    var result = await _userPostService.Post(userDto);
                    return result;

                }
                else
                {
                    return BadRequest("Error");
                }

            }
        }
    }

    //Este metodo no hace ningun check al Query ya que asume que esta ingresado correctamente
    [HttpGet("ExecQuery")]
    public async Task<ActionResult> ExecQuery(string query, int instanceId, string saPassword)
    {
        var instance = await _repository.GetAsync(x => x.InstanceID == instanceId);
        QueryExec queryExec = new QueryExec($"Server=localhost\\{instance.InstanceName.ToUpper()};Database=master;User Id=sa;Password={saPassword};TrustServerCertificate=True;");
        await queryExec.Execute(query);
        return Ok();
    }
    
}