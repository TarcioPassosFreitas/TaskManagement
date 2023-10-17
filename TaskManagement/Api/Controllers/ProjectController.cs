using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Aggregates;
using TaskManagement.Core.Interfaces.Services;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _projectService.GetByIdAsync(id, cancellationToken);
        if (result.Status == ResultStatus.NotFound)
            return NotFound(result.ValidationErrors);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var result = await _projectService.ListAsync(cancellationToken);
        if (result.Status == ResultStatus.NotFound)
            return NotFound(result.ValidationErrors);

        return Ok(result.Value);
    }

    [HttpGet("search/{name}")]
    public async Task<IActionResult> SearchByName(string name, CancellationToken cancellationToken)
    {
        var result = await _projectService.SearchByNameAsync(name, cancellationToken);
        if (result.Status == ResultStatus.NotFound)
            return NotFound(result.ValidationErrors);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Project project, CancellationToken cancellationToken)
    {
        var result = await _projectService.AddAsync(project, cancellationToken);
        if (result.Status == ResultStatus.Invalid)
            return BadRequest(result.ValidationErrors);

        return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Project project, CancellationToken cancellationToken)
    {
        if (id != project.Id)
            return BadRequest("ID da URL e ID do corpo da requisição devem ser iguais.");

        var result = await _projectService.UpdateAsync(project, cancellationToken);
        if (result.Status == ResultStatus.NotFound)
            return NotFound(result.ValidationErrors);
        if (result.Status == ResultStatus.Invalid)
            return BadRequest(result.ValidationErrors);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _projectService.DeleteAsync(id, cancellationToken);
        if (result.Status == ResultStatus.NotFound)
            return NotFound(result.ValidationErrors);
        if (result.Status == ResultStatus.Invalid)
            return BadRequest(result.ValidationErrors);

        return NoContent();
    }
}