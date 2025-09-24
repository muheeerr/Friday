using Friday.Abstractions;
using Friday.Api.Samples;
using Friday.Core;
using Microsoft.AspNetCore.Mvc;

namespace Friday.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PingController : ControllerBase
{
    private readonly IFriday _friday;
    
    public PingController(IFriday friday)
    {
        _friday = friday;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
       // var a = _ac.Abc();
        var response = await _friday.Send(new PingRequest { Message = "From Controller" });
        return Ok(response);
    }
}
