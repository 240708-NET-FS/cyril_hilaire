using ContactBook.Controller;
using ContactBook.DAO;
using ContactBook.Entities;
using ContactBook.Service;
using ContactBook.Classes;

namespace ContactBook;

class Program
{
    static void Main(string[] args)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();

        using(var ctx = new AppDbContext())
        {
            ContactDAO contactDAO = new ContactDAO(ctx);
            PhoneDAO phoneDAO = new PhoneDAO(ctx);
            //
            Console.WriteLine("...");

            SplashscreenClass splashscreen = new SplashscreenClass();
            // Welcome screen
            splashscreen.Display("");

            // Instantiate services
            ContactService contactService = new ContactService(contactDAO);
            PhoneService phoneService = new PhoneService(phoneDAO);

            // Contact Controller
            ContactController contactConntroller = new ContactController(contactService, phoneService);
            // contactConntroller.SetPhoneService(phoneService);

            // Thank You screen
            splashscreen.Display("./thank_you_screen.txt");
            
            Console.WriteLine("...");
            //
            // Contact contact = new Contact(){ FirstName = "Jhon", LastName = "Doe" };
            // contactDAO.Create(contact);
            // Console.WriteLine(contact);
            //
            // phoneDAO.Create(new Phone(){ PhoneNumber = "1112223333", ContactId = contact.Id });
            // phoneDAO.Create(new Phone(){ PhoneNumber = "4445556666", ContactId = contact.Id });
            //
            /*
            ICollection<Contact> contacts = contactDAO.GetAll();
            foreach (Contact myContact in contacts)
            {
                Console.WriteLine($"{myContact.FirstName} {myContact.LastName} {myContact.Email} {myContact.CreatedDate}");
                foreach(Phone myPhone in myContact.Phones)
                {
                    Console.WriteLine($"{myPhone.Id} - {myPhone.PhoneNumber} - {myPhone.CreatedDate}");
                }
            }
            */
        }

        // the code that you want to measure comes here
        watch.Stop();
        TimeSpan ts = TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds);
        var elapsedTime = ts.ToString(@"hh\:mm\:ss");
        Console.WriteLine( 
          $"The Execution time of the program is {elapsedTime} ms");
        Console.WriteLine("...");
    }
}
