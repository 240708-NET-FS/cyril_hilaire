using System.Security.Cryptography;
using ContactBook.DAO;
using ContactBook.Entities;

namespace ContactBook.Service;

public class PhoneService : IService<Phone>
{
    private readonly PhoneDAO _phoneDAO;
    public PhoneService(PhoneDAO pDAO)
    {
        _phoneDAO = pDAO;
    }

    public void Create(Phone entity)
    {
        if(InputValidation.CheckPhone(entity.PhoneNumber) != true)
        {
            throw new Exception("Input Phone Validation Error.");
        }
        _phoneDAO.Create(entity);
    }

    public void Delete(Phone entity)
    {
        if(_phoneDAO.GetByID(entity.Id) != null)
        {
            _phoneDAO.Delete(entity);
        }
        else
        {
            throw new Exception("Phone number does not exist.");
        }
    }

    public ICollection<Phone> GetAll()
    {
        return _phoneDAO.GetAll();
    }

    public ICollection<Phone> GetPhonesByContactID(int contactID)
    {
        return _phoneDAO.GetPhonesByContactID(contactID);
    }

    public Phone GetById(int Id)
    {
        return _phoneDAO.GetByID(Id);
    }

    public bool Update(Phone entity)
    {
        return _phoneDAO.Update(entity);
    }
}