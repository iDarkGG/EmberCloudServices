using AutoMapper;
using EmberAPI.Dtos;
using EmberAPI.Models;
using EmberAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmberAPI.Controllers;
[Route("EmberAPI/[controller]")]
[ApiController]

public class InstanceController : Controller
{
    private readonly IInstanceRepository _repository;
    private readonly IMapper _mapper;

    public InstanceController(IInstanceRepository repository, IMapper mapper)
    {
        this._repository = repository;
        _mapper = mapper;
    }

    [HttpGet("ListInstances")]
    public async Task<ActionResult<IEnumerable<InstanceDto>>> GetClusters()
    {
        var clusters = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<InstanceDto>>(clusters));
    }
}