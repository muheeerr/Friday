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
    private readonly IAbc _ac;

    public PingController(IFriday friday, IAbc ac)
    {
        _friday = friday;
        _ac = ac;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
       var a = _ac.Abc();
        var response = await _friday.Send(new PingRequest { Message = "From Controller" });
        return Ok(response);
    }
}
