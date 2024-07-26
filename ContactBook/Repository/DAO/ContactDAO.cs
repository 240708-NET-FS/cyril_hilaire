using System;
using ContactBook.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.DAO;

public class ContactDAO : IDAO<Contact>
{
    private AppDbContext _ctx;
    public ContactDAO(AppDbContext appDbContext)
    {
        _ctx = appDbContext;
    }

    public void Create(Contact item)
    {
        item.CreatedDate = DateTime.Now;
        _ctx.Contacts.Add(item);
        _ctx.SaveChanges();
    }

    public void Delete(Contact item)
    {
        _ctx.Contacts.Remove(item);
        _ctx.SaveChanges();
    }

    public ICollection<Contact> GetAll()
    {
        List<Contact> contacts = _ctx.Contacts.Include(p => p.Phones)
                                .ToList();
        return contacts;
    }

    public ICollection<Contact> GetByFirstnameOrLastname(string userStr)
    {
        List<Contact> contacts = _ctx.Contacts
                                .Where(c => EF.Functions.Like(c.FirstName, userStr + "%") || EF.Functions.Like(c.LastName, userStr + "%"))
                                .Include(p => p.Phones)
                                .ToList();
        return contacts;
    }

    public Contact GetByID(int ID)
    {
        Contact contact = _ctx.Contacts.FirstOrDefault(c => c.Id == ID);
        return contact;
    }

    public bool Update(Contact newItem)
    {
        Contact originalContact = _ctx.Contacts.FirstOrDefault(c => c.Id == newItem.Id);
        if(originalContact != null)
        {
            originalContact.FirstName = newItem.FirstName;
            originalContact.LastName = newItem.LastName;
            originalContact.Email = newItem.Email;
            //
            _ctx.Contacts.Update(originalContact);
            _ctx.SaveChanges(); 
            return true;
        }

        return false;
    }
}