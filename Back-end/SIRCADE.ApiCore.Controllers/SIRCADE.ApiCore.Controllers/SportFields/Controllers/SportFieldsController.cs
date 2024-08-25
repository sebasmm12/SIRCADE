using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.SportFields.Requests;
using SIRCADE.ApiCore.Controllers.SportFields.Services;
using SIRCADE.ApiCore.Models.Common.Queries;

namespace SIRCADE.ApiCore.Controllers.SportFields.Controllers;

[ApiController]
[Route("api/sport-fields")]
public class SportFieldsController(ISportFieldsService sportFieldsService) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAsync()
    {
        var sportFields = await sportFieldsService.GetAsync();

        return Ok(sportFields);
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] DataTableQueriesDto dataTableQueries)
    {
        var sportFields = await sportFieldsService.GetAsync(dataTableQueries);

        return Ok(sportFields);
    }

    [HttpGet("{sportFieldId:int}")]
    public async Task<IActionResult> GetAsync(int sportFieldId)
    {
        var sportField = await sportFieldsService.GetAsync(sportFieldId);

        return Ok(sportField);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] SportFieldCreationRequest sportFieldCreationRequest)
    {
        var sportFieldId = await sportFieldsService.CreateAsync(sportFieldCreationRequest);

        return Ok(sportFieldId);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] SportFieldUpdateRequest sportFieldUpdateRequest)
    {
        await sportFieldsService.UpdateAsync(sportFieldUpdateRequest);

        return Ok();
    }
}