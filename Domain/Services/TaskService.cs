using Domain.Interfaces;
using Domain.Models;

namespace Domain.Services;

public class TaskService
{
    private readonly IRepository<TaskModel> _tasksRepository;

    public TaskService(IRepository<TaskModel> tasksRepository)
    {
        _tasksRepository = tasksRepository;
    }

    public TaskModel AddTask(TaskModel task)
    {
        return _tasksRepository.Create(task);
    }

    public List<TaskModel> GetAllTasks()
    {
        return _tasksRepository.GetAll();
    }

    public bool TryCompleteTaskById(int taskId)
    {
        var taskToComplete = _tasksRepository.GetFirstOrDefault(taskId);

        if (taskToComplete is null) 
            return false;

        taskToComplete.IsCompleted = true;

        _tasksRepository.Update(taskToComplete);

        return true;
    }

    public bool TryDeleteTaskById(int taskId)
    {
        return _tasksRepository.Delete(taskId);
    }

}
