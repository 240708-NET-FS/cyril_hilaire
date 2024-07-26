using System.ComponentModel.DataAnnotations;

namespace ContactBook.Entities;

public class Contact
{
    public int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public string? Email { get; set; }

    public ICollection<Phone> Phones { get; } = []; // Collection navigation containing dependents

    public DateTime CreatedDate { get; set;}

    public override string ToString() => $"{Id} - {FirstName} - {LastName} - {Email} - {CreatedDate} - {Phones}";
}