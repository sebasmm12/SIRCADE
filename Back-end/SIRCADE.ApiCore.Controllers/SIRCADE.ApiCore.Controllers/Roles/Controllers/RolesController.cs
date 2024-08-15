using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.Roles.Requests;
using SIRCADE.ApiCore.Controllers.Roles.Services;
using SIRCADE.ApiCore.Models.Common.Queries;

namespace SIRCADE.ApiCore.Controllers.Roles.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController(IRolesService rolesService) : ControllerBase
{

    [HttpGet("all")]
    public async Task<IActionResult> GetAsync()
    {
        var roles = await rolesService.GetAsync();

        return Ok(roles);
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAsync([FromQuery] DataTableQueriesDto dataTableQueries)
    {
        var roles = await rolesService.GetAsync(dataTableQueries);

        return Ok(roles);
    }

    [HttpGet("{roleId:int}")]
    public async Task<IActionResult> GetAsync(int roleId)
    {
        var role = await rolesService.GetAsync(roleId);

        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] RoleCreationRequest roleCreationRequest)
    {
        var roleId = await rolesService.CreateAsync(roleCreationRequest);

        return Ok(roleId);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] RoleUpdateRequest roleUpdateRequest)
    {
        await rolesService.UpdateAsync(roleUpdateRequest);

        return Ok();
    }

    [HttpPut("{roleId:int}/status")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int roleId)
    {
        await rolesService.UpdateStatusAsync(roleId);

        return Ok();
    }

    [HttpDelete("{roleId:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int roleId)
    {
        await rolesService.DeleteAsync(roleId);

        return Ok();
    }
}