using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Aggregates;
using TaskManagement.Core.Interfaces.Services;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskItemController : ControllerBase
{
    private readonly ITaskItemService _taskItemService;

    public TaskItemController(ITaskItemService taskItemService)
    {
        _taskItemService = taskItemService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _taskItemService.GetByIdAsync(id, cancellationToken);
        if (result.Status == ResultStatus.NotFound)
            return NotFound(result.ValidationErrors);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var result = await _taskItemService.ListAsync(cancellationToken);
        if (result.Status == ResultStatus.NotFound)
            return NotFound(result.ValidationErrors);

        return Ok(result.Value);
    }

    [HttpGet("search/{name}")]
    public async Task<IActionResult> SearchByName(string name, CancellationToken cancellationToken)
    {
        var result = await _taskItemService.SearchByNameAsync(name, cancellationToken);
        if (result.Status == ResultStatus.NotFound)
            return NotFound(result.ValidationErrors);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskItem taskItem, CancellationToken cancellationToken)
    {
        var result = await _taskItemService.AddAsync(taskItem, cancellationToken);
        if (result.Status == ResultStatus.Invalid)
            return BadRequest(result.ValidationErrors);

        return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, TaskItem taskItem, CancellationToken cancellationToken)
    {
        if (id != taskItem.Id)
            return BadRequest("ID da URL e ID do corpo da requisição devem ser iguais.");

        var result = await _taskItemService.UpdateAsync(taskItem, cancellationToken);
        if (result.Status == ResultStatus.NotFound)
            return NotFound(result.ValidationErrors);
        if (result.Status == ResultStatus.Invalid)
            return BadRequest(result.ValidationErrors);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _taskItemService.DeleteAsync(id, cancellationToken);
        if (result.Status == ResultStatus.NotFound)
            return NotFound(result.ValidationErrors);
        if (result.Status == ResultStatus.Invalid)
            return BadRequest(result.ValidationErrors);

        return NoContent();
    }
}
