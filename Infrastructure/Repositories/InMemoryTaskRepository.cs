using Backend.Core.Entities;
using Backend.Core.Interfaces;

namespace Backend.Infrastructure.Repositories;

public class InMemoryTaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = new();
    private int _nextId = 1;

    public IEnumerable<TaskItem> GetAll() => _tasks;

    public TaskItem? GetById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

    public void Add(TaskItem task)
    {
        task.Id = _nextId++;
        _tasks.Add(task);
    }

    public void Update(TaskItem task)
    {
        var existing = GetById(task.Id);
        if (existing != null)
        {
            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.Status = task.Status;
        }
    }

    public void Delete(int id)
    {
        var task = GetById(id);
        if (task != null)
            _tasks.Remove(task);
    }
}