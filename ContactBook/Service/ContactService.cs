using System;
using ContactBook.DAO;
using ContactBook.Entities;

namespace ContactBook.Service;

public class ContactService : IService<Contact>
{
    private readonly ContactDAO _contactDAO;

    public ContactService(ContactDAO contactDAO)
    {
        _contactDAO = contactDAO;
    }

    public void Create(Contact entity)
    {
        if(InputValidation.CheckFirstname(entity.FirstName) != true || 
        InputValidation.CheckLastname(entity.LastName) != true || 
        InputValidation.CheckEmail(entity.Email) != true)
        {
            throw new Exception("Input Validation Error.");
        }
        _contactDAO.Create(entity);
    }

    public void Delete(Contact entity)
    {
        if(_contactDAO.GetByID(entity.Id) != null)
        {
            _contactDAO.Delete(entity);
        }
        else
        {
            throw new Exception("Contact does not exist.");
        }
    }

    public ICollection<Contact> GetAll()
    {
        return _contactDAO.GetAll();
    }

    public ICollection<Contact> GetByFirstnameOrLastname(string userStr)
    {
        return _contactDAO.GetByFirstnameOrLastname(userStr);
    }


    public Contact GetById(int Id)
    {
        return _contactDAO.GetByID(Id);
    }

    public bool Update(Contact entity)
    {
        return _contactDAO.Update(entity);
    }
}