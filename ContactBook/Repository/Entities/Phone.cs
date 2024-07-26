using System.ComponentModel.DataAnnotations;

namespace ContactBook.Entities;

public class Phone
{
    public int Id { get; set; }

    public required string PhoneNumber { get; set; }

    public required int ContactId { get; set; }

    public Contact Contacts{ get; set; }

    public DateTime CreatedDate { get; set; }
   //  public DateTime CreatedDate { get; set;}

   public override string ToString() => $"{Id} - {PhoneNumber} - {ContactId} - {CreatedDate}";
}