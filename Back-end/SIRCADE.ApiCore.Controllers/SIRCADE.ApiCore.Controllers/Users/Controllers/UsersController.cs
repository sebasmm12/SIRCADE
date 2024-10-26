using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.Users.Requests;
using SIRCADE.ApiCore.Controllers.Users.Services;
using SIRCADE.ApiCore.Models.Users.DTOs;

namespace SIRCADE.ApiCore.Controllers.Users.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UsersController(IUsersService usersService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] UserDataTableQueriesDto userDataTableQueries)
    {
        try
        {
            var users = await usersService.GetAsync(userDataTableQueries);

            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> GetAsync([FromRoute] int userId)
    {
        try
        {
            var user = await usersService.GetAsync(userId);

            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{nsa:int}/validation")]
    public async Task<IActionResult> ValidateAsync([FromRoute] int nsa)
    {
        try
        {
            var isNsaValid = await usersService.ValidateNsaAsync(nsa);

            return Ok(isNsaValid);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] UserCreationRequest user)
    {
        try
        {
            var userId = await usersService.CreateAsync(user);

            return Ok(userId);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UserUpdateRequest user)
    {
        try
        {
            await usersService.UpdateAsync(user);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{userId:int}/status")]
    public async Task<IActionResult> UpdateActiveAsync([FromRoute] int userId)
    {
        try
        {
            await usersService.UpdateActiveAsync(userId);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}