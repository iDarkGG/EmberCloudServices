using AutoMapper;
using EmberAPI.BackgroundServices;
using EmberAPI.Context;
using EmberAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EmberAPI.Controllers;

[Route("EmberAPI/[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
    private readonly MainContext _context;
    private static double _dbSize;
    private static DateTime _timestamp;
    
    public ServicesController(MainContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public IActionResult RecieveDbSize([FromBody] DbSizeDto size)
    {
        Console.WriteLine($"Recieved size: {size.DataBaseSize}, at {size.Timestamp}");
        _dbSize = size.DataBaseSize;
        _timestamp = size.Timestamp;
        return Ok();
    }

    [HttpGet("RetrieveBdSize")]
    public IActionResult RetrieveBdSize()
    {
        var output = new DbSizeDto()
        {
            DataBaseSize = _dbSize,
            Timestamp = _timestamp
        };
        return Ok(output);
    }
}