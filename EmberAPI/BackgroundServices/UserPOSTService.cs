using AutoMapper;
using EmberAPI.Dtos;
using EmberAPI.Models;
using EmberAPI.Repositories;
using EmberCloudServices;
using Microsoft.AspNetCore.Mvc;

namespace EmberAPI.BackgroundServices;

// ReSharper disable once InconsistentNaming
public class UserPOSTService: Controller
{
    private StringHasher hasher = new StringHasher();
    private readonly ICreatedUserRepository _context;
    private readonly IMapper _mapper;
    
    public UserPOSTService(ICreatedUserRepository context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ActionResult> Post([FromBody] CreatedUserPOSTDto user)
    {
        var username = hasher.EncryptString(user.userNameHash);
        var password = hasher.EncryptString(user.userPasswordHash);
        user.userNameHash = username;
        user.userPasswordHash = password;

        var searchUser = await _context.GetAsync(p => p.userNameHash == user.userNameHash);
        if (searchUser is not null) return BadRequest("1");
        
        if (user is null) return BadRequest("2");
        
        await _context.Add(_mapper.Map<CreatedUser>(user));

        return Ok();
    }
}