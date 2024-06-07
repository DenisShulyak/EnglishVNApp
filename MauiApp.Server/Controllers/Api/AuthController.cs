using Microsoft.AspNetCore.Mvc;

namespace MauiApp.Server.Controllers.Api;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost]
    public Task<ActionResult> Login(string email)
    {
        return Task.FromResult<ActionResult>(Ok());
    }
}