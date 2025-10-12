using Dapper;
using DataAccess;
using DataAccess.ConnectionFactories;
using DataAccess.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.Metrics;

namespace TaskManager;

internal class Program
{
    static void Main(string[] args)
    {
        //string connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection")!;
        string connectionString = new ConfigurationBuilder().AddUserSecrets<Program>().Build().GetConnectionString("DefaultConnection")!;

        var taskMaster = new TaskMaster(new TasksRepository(new SqlServerConnectionFactory(connectionString)));

        Console.WriteLine("\nInitial state:");
        DisplayTasks(taskMaster.GetAllTasks());

        var newTask = taskMaster.AddTask(new TaskModel() { Title = "Groceries", Description = "Buy milk, eggs and beer" });
        Console.WriteLine("\nAdding new task:");
        DisplayTasks(taskMaster.GetAllTasks());

        taskMaster.TryCompleteTaskById(newTask.Id);
        Console.WriteLine("\nCompleting it:");
        DisplayTasks(taskMaster.GetAllTasks());

        taskMaster.TryDeleteTaskById(newTask.Id);
        Console.WriteLine("\nDeleting it:");
        DisplayTasks(taskMaster.GetAllTasks());
    }

    private static void DisplayTasks(List<TaskModel> tasks)
    {
        foreach (var task in tasks)
        {
            string isCompletedEmoji = task.IsCompleted ? "V" : "X";
            Console.WriteLine($"{task.Id}) {task.Title}: {task.Description} ({isCompletedEmoji})");
        }
        Console.WriteLine($"Tasks total: {tasks.ToArray().Length}");
    }
}
