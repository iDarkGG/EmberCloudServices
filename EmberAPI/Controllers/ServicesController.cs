using EmberAPI.Context;
using EmberAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EmberAPI.Controllers;

[Route("EmberAPI/[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
    private readonly MainContext _context;
    
    public ServicesController(MainContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public IActionResult RecieveDbSize([FromBody] DbSizeDto size)
    {
        Console.WriteLine($"Recieved size: {size.DataBaseSize}, at {size.Timestamp}");
        return Ok();
    }
}