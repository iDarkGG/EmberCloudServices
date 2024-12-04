using AutoMapper;
using EmberAPI.BackgroundServices;
using EmberAPI.Dtos;
using EmberAPI.Models;
using EmberAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using EmberCloudServices;
using Microsoft.AspNetCore.JsonPatch;

namespace EmberAPI.Controllers
{
    [Route("EmberAPI/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private StringHasher hasher = new StringHasher(); 
        private readonly ICreatedUserRepository _context;
        private readonly IMapper _mapper;
        private UserPOSTService _userPostService;


        
        [ActivatorUtilitiesConstructor]
        public UserController(IMapper? mapper, ICreatedUserRepository? repository, UserPOSTService userPostService)
        {
            _mapper = mapper;
            this._context = repository;
            _userPostService = userPostService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreatedUser>>> GetUsers()
        {
            var lst = await _context.GetAllAsync();
            if (lst == null) return NoContent();
            return Ok(_mapper.Map<List<CreatedUserDto>>(lst));
        }

        [HttpGet("get-users-by-name/{username}")]
        public async Task<ActionResult> GetUserByName(string username)
        {
            if (username == null) return BadRequest();
            var result = await _context.GetAsync(x => x.userNameHash == hasher.EncryptString(username));
            
            if (result is null) return NotFound();

            return Ok(_mapper.Map<CreatedUserDto>(result));
        }

        [HttpPost("create-user")]

        public async Task<ActionResult> PostUser([FromBody] CreatedUserPOSTDto user)
        {
            var result = await _userPostService.Post(user);
            return result;
        }

        [HttpPatch("patch-user/{name}")]
        public async Task<ActionResult> PatchUser([FromBody] JsonPatchDocument<CreatedUserDto> userPatch, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("User name is required.");

            if (userPatch is null)
                return BadRequest("Patch document cannot be null.");

            var userEntity = await _context.GetAsync(x => x.userNameHash == hasher.EncryptString(name));
            if (userEntity is null)
                return NotFound($"User with name '{name}' not found.");
            userEntity.userNameHash = hasher.EncryptString(userEntity.userNameHash);
            userEntity.userPasswordHash = hasher.EncryptString(userEntity.userPasswordHash);

            var CreatedUserDto = _mapper.Map<CreatedUserDto>(userEntity);

            userPatch.ApplyTo(CreatedUserDto, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _mapper.Map(CreatedUserDto, userEntity);

            await _context.UpdateUser(userEntity);

            return Ok("User successfully updated.");
        }

        [HttpDelete("delete-user/{name}")]
        public async Task<ActionResult> DeleteUser(string name)
        {
            var username = hasher.EncryptString(name);
            if (string.IsNullOrWhiteSpace(name)) return BadRequest("User name is required.");
            var userEntity = await _context.GetAsync(x => x.userNameHash == username);
            if (userEntity is null) return NotFound($"User with name '{name}' not found.");
            
            await _context.DeleteAsync(userEntity);
            return Ok("User successfully deleted.");
        }

        [HttpDelete("delete-users-by-instanceid/{instanceid}")]
        public async Task<ActionResult> DeleteUsersByInstanceId(int instanceid)
        {
            await _context.DeleteAllUserByInstanceId(instanceid);
            return Ok("Users successfully deleted.");
        }
    }   
}
