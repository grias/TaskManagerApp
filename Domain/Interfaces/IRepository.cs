namespace Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    public List<TEntity> GetAll();

    public TEntity? GetFirstOrDefault(int entityId);

    public TEntity Create(TEntity newEntity);

    public TEntity Update(TEntity entityToModify);

    public bool Delete(int entityId);
}
