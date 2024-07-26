using ContactBook.Entities;

namespace ContactBook.DAO;

public class PhoneDAO : IDAO<Phone>
{
    private AppDbContext _ctx;

    public PhoneDAO(AppDbContext appDbContext)
    {
        _ctx = appDbContext;
    }

    public void Create(Phone item)
    {
        item.CreatedDate = DateTime.Now;
        _ctx.Phones.Add(item);
        _ctx.SaveChanges();
    }

    public void Delete(Phone item)
    {
        _ctx.Phones.Remove(item);
        _ctx.SaveChanges();
    }

    public ICollection<Phone> GetAll()
    {
        List<Phone> phones = _ctx.Phones.ToList();
        return phones;
    }

    public ICollection<Phone> GetPhonesByContactID(int contactID)
    {
        List<Phone> phones = _ctx.Phones
                                .Where(p => p.ContactId == contactID)
                                .ToList();
        return phones;
    }

    public Phone GetByID(int ID)
    {
        Phone phone = _ctx.Phones.FirstOrDefault(p => p.Id == ID);
        return phone;
    }

    public bool Update(Phone newItem)
    {
        Phone originalPhone = _ctx.Phones.FirstOrDefault(c => c.Id == newItem.Id);
        if(originalPhone != null)
        {
            originalPhone.PhoneNumber = newItem.PhoneNumber;
            //
            _ctx.Phones.Update(originalPhone);
            _ctx.SaveChanges(); 
            return true;
        }

        return false;
    }
}