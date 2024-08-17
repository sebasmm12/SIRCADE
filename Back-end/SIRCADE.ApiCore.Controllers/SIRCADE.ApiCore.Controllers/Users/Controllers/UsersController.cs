using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.Users.Requests;
using SIRCADE.ApiCore.Controllers.Users.Services;

namespace SIRCADE.ApiCore.Controllers.Users.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUsersService usersService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(UserCreationRequest user)
    {
        var userId = await usersService.CreateAsync(user);

        return Ok(userId);
    }
}