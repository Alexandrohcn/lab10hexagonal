namespace Lab10_AlexandroCano.Application.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> GetByIdAsync(Guid id);

    Task AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);
}

//las interfaces deben de estar en dominio no en aplicattion