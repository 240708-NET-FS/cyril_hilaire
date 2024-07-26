namespace ContactBook.Service;

public interface IService<T>
{
    public T GetById(int Id);

    public ICollection<T> GetAll();

    public void Create(T entity);

    public bool Update(T entity);

    public void Delete(T entity);
}