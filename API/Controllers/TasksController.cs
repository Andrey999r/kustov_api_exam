using Backend.Core.DTOs;
using Backend.Core.Entities;
using Backend.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskRepository _repository;

    public TasksController(ITaskRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_repository.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var task = _repository.GetById(id);
        return task == null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public IActionResult Create([FromBody] TaskDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status
        };

        _repository.Add(task);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] TaskDto dto)
    {
        var existing = _repository.GetById(id);
        if (existing == null) return NotFound();

        existing.Title = dto.Title;
        existing.Description = dto.Description;
        existing.Status = dto.Status;

        _repository.Update(existing);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var task = _repository.GetById(id);
        if (task == null) return NotFound();

        _repository.Delete(id);
        return NoContent();
    }
}