namespace ContactBook.DAO;

public interface IDAO<T>
{
    // Create
    public void Create(T item);

    // Read
    public T GetByID(int ID);

    public ICollection<T> GetAll();

    // Update
    public bool Update(T newItem);

    // Delete
    public void Delete(T item);
}