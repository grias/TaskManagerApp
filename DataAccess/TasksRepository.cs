
using Dapper;
using DataAccess.ConnectionFactories;
using DataAccess.Models;

namespace DataAccess;

public class TasksRepository : IRepository<TaskModel>
{
    private readonly IConnectionFactory _connectionFactory;

    public TasksRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public TaskModel Create(TaskModel newTask)
    {
        var connection = _connectionFactory.CreateConnection();
        string sqlCommand = "INSERT INTO Tasks (Title, Description) OUTPUT INSERTED.Id VALUES (@Title, @Description)";
        var createdTaskId = connection.QuerySingle<int>(sqlCommand, newTask);
        var createdTask = GetFirstOrDefault(createdTaskId);
        return createdTask!;
    }

    public List<TaskModel> GetAll()
    {
        var connection = _connectionFactory.CreateConnection();
        string sqlCommand = "SELECT * FROM Tasks";
        return connection.Query<TaskModel>(sqlCommand).ToList<TaskModel>();
    }

    public TaskModel? GetFirstOrDefault(int entityId)
    {
        var connection = _connectionFactory.CreateConnection();
        string sqlCommand = "SELECT * FROM Tasks WHERE Id = @Id";
        return connection.QueryFirstOrDefault<TaskModel>(sqlCommand, new { Id = entityId });
    }

    public bool Delete(int entityId)
    {
        var connection = _connectionFactory.CreateConnection();
        string sqlCommand = "DELETE FROM Tasks WHERE Id = @Id";
        var numberOfDeletedEntities = connection.Execute(sqlCommand, new { Id = entityId });
        return numberOfDeletedEntities > 0;
    }

    public TaskModel Update(TaskModel entityToModify)
    {
        var connection = _connectionFactory.CreateConnection();
        string sqlCommand = @"
                            UPDATE Tasks 
                            SET Title = @Title, Description = @Description, IsCompleted = @IsCompleted
                            WHERE Id = @Id";
        var numberOfDeletedEntities = connection.Execute(sqlCommand, entityToModify);
        return GetFirstOrDefault(entityToModify.Id)!;
    }
}