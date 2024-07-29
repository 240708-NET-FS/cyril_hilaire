using System;
using ContactBook.Entities;
using ContactBook.Service;
using Microsoft.IdentityModel.Tokens;

namespace ContactBook.Controller;

public class ContactController
{
    private ContactService contactService;
    private PhoneService phoneService;

    public const string OPT_EXIT_KEY = "0";
    public const string OPT_EXIT_VALUE = "Exit";
    public const string OPT_CREATE_NEW_KEY = "1";
    public const string OPT_CREATE_NEW_VALUE = "Create New Contact";
    public const string OPT_VIEW_ALL_KEY = "2";
    public const string OPT_VIEW_ALL_VALUE = "View All Contact";
    public const string OPT_VIEW_SEARCH_KEY = "3";
    public const string OPT_VIEW_SEARCH_VALUE = "Search a contact";

    // Sub menu
    public const string LABEL_CREATE_NEW_FIRSTNAME = "First name*: ";
    public static string INPUT_CREATE_NEW_FIRSTNAME = "";
    public const string LABEL_CREATE_NEW_LASTNAME = "Last name*: ";
    public static string INPUT_CREATE_NEW_LASTNAME = "";
    public const string LABEL_CREATE_NEW_PHONE = "Phone*: ";
    public static string INPUT_CREATE_NEW_PHONE = "";
    public const string LABEL_CREATE_NEW_EMAIL = "E-mail: ";
    public static string INPUT_CREATE_NEW_EMAIL = "";

    public ContactController(ContactService cService, PhoneService pService)
    {
        contactService = cService;
        phoneService = pService;
        MainMenu();
    }

    /*
    public void SetPhoneService(PhoneService pService)
    {
        phoneService = pService;
    }
    */

    /**
    * Display the main menu of the phonebook.
    */
    public void MainMenu()
    {
        Dictionary<string, string> menuDict = new Dictionary<string, string>(); 
           
        // Adding menu option in the Dictionary Using Add() method
        menuDict.Add(OPT_CREATE_NEW_KEY, OPT_CREATE_NEW_VALUE);
        menuDict.Add(OPT_VIEW_ALL_KEY, OPT_VIEW_ALL_VALUE);
        menuDict.Add(OPT_VIEW_SEARCH_KEY, OPT_VIEW_SEARCH_VALUE);
        menuDict.Add(OPT_EXIT_KEY, OPT_EXIT_VALUE);

        string? choiceStr = "";

        // Loop until user choose a correct option
        do
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("What do you want to do?");
            foreach(KeyValuePair<string, string> ele1 in menuDict)
            {
                Console.WriteLine("{0}) \t {1}", ele1.Key, ele1.Value);
            }

            int choiceInt = 0;
            
            Console.WriteLine("");
            Console.WriteLine("");

            Console.Write("Choose a number:");
            choiceStr = Console.ReadLine();

            // Convert user choice to int and catch error
            bool isParsable = Int32.TryParse(choiceStr, out choiceInt);
            if (!isParsable)
            {
                // Console.WriteLine($"'{choiceStr}' is not a number.");
                choiceInt = 0;
            }

            switch (choiceStr)
            {
            case OPT_CREATE_NEW_KEY:
                Console.WriteLine("");
                Console.WriteLine("\t{0}", OPT_CREATE_NEW_VALUE);
                Console.WriteLine("");
                CreateContactMenu();
                break;
            case OPT_VIEW_ALL_KEY:
                Console.WriteLine("");
                Console.WriteLine("\t{0}", OPT_VIEW_ALL_VALUE);
                Console.WriteLine("");
                ViewAllContacts();
                break;
            case OPT_VIEW_SEARCH_KEY:
                Console.WriteLine("");
                Console.WriteLine("\t{0}", OPT_VIEW_SEARCH_VALUE);
                Console.WriteLine("");
                SearchContact();
                break;
            case OPT_EXIT_KEY:
                Console.WriteLine(OPT_EXIT_VALUE);
                break;
            default:
                Console.WriteLine($"'{choiceStr}' is not an option.");
                break;
            }

        }while(choiceStr != OPT_EXIT_KEY);
    }

    /**
    * Create new contact
    */
    public void CreateContactMenu()
    {
        List<string> phoneObj = new List<string>();

        string? userInputStr = "";
        // Firstname
        do{
            Console.Write(LABEL_CREATE_NEW_FIRSTNAME +" : ");
            userInputStr = Console.ReadLine();
        }while(InputValidation.CheckFirstname(userInputStr) != true);

        INPUT_CREATE_NEW_FIRSTNAME = InputValidation.FormatInputValue(userInputStr);

        // Lastname
        do
        {
            Console.Write(LABEL_CREATE_NEW_LASTNAME +" : ");
            userInputStr = Console.ReadLine();
        }while(InputValidation.CheckLastname(userInputStr) != true);

        INPUT_CREATE_NEW_LASTNAME = InputValidation.FormatInputValue(userInputStr);

        // Phone
        do
        {
            Console.Write(LABEL_CREATE_NEW_PHONE +" : ");
            userInputStr = Console.ReadLine();
            if(InputValidation.CheckPhone(userInputStr) == true)
            {
                phoneObj.Add(userInputStr);
            }
        }while(!string.IsNullOrEmpty(userInputStr));

        INPUT_CREATE_NEW_PHONE = InputValidation.FormatInputValue(userInputStr);

        // Email
        do
        {
            Console.Write(LABEL_CREATE_NEW_EMAIL +" : ");
            userInputStr = Console.ReadLine();
        }while(InputValidation.CheckEmail(userInputStr) != true);

        INPUT_CREATE_NEW_EMAIL = InputValidation.FormatInputValue(userInputStr);

        // Store in DB
        if(phoneObj.IsNullOrEmpty())
        {
            throw new Exception("Phone number cannot be empty");
        }
        else
        {
            Contact contact = new Contact
            {
                FirstName = INPUT_CREATE_NEW_FIRSTNAME,
                LastName = INPUT_CREATE_NEW_LASTNAME,
                Email = INPUT_CREATE_NEW_EMAIL
            };
            contactService.Create(contact);
            //
            foreach (var num in phoneObj)
            {
                phoneService.Create(new Phone { PhoneNumber = num, ContactId = contact.Id });
            }
        }
        
        // Console.WriteLine("Have been saved...");
        Console.WriteLine("{0}, {1} has been added successfully.", 
        INPUT_CREATE_NEW_FIRSTNAME, 
        INPUT_CREATE_NEW_LASTNAME);
    }

    /**
    * List alll contacts in the phonebook.
    */
    public void ViewAllContacts()
    {
        ICollection<Contact> contacts = contactService.GetAll();
        if (contacts == null || contacts.Count < 1)
        {
            Console.WriteLine("NO CONTACT");
        }
        else
        {
            var i = 0;
            foreach (Contact myContact in contacts)
            {
                i++;
                Console.WriteLine($"{i}.- {myContact.FirstName} {myContact.LastName.ToUpper()} {myContact.Email}");
                int j = 1;
                foreach(Phone myPhone in myContact.Phones)
                {
                    Console.WriteLine($"\t- {myPhone.PhoneNumber}");
                }
                Console.WriteLine("");
            }
        }
    }

    /**
    * Search contact by first name or last name.
    */
    public void SearchContact()
    {
        Console.Write("Search by first name or last name: ");
        string? userInputStr = Console.ReadLine();

        if(!userInputStr.IsNullOrEmpty())
        {
            ICollection<Contact> contacts = contactService.GetByFirstnameOrLastname(userInputStr);
            
            if (contacts == null || contacts.Count < 1)
            {
                Console.WriteLine("NO CONTACT FOUND");
            }
            else
            {
                List<Contact> contactsList = contacts.ToList();
                var i = 0;
                foreach (Contact myContact in contacts)
                {
                    i++;
                    Console.WriteLine($"{i}.- {myContact.FirstName} {myContact.LastName.ToUpper()} {myContact.Email}");
                    int j = 1;
                    foreach(Phone myPhone in myContact.Phones)
                    {
                        Console.WriteLine($"\t => {myPhone.PhoneNumber}");
                    }
                    Console.WriteLine("");
                }

                // Choose a contact
                string? choiceSearchStr = null;
                bool flagChooseContact = false;
                int choiceContactInt = 0;

                do{
                    Console.WriteLine("");
                    Console.WriteLine("Choose a contact: ");

                    Console.Write("#:");
                    choiceSearchStr = Console.ReadLine();

                    // Convert user choice to int and catch error
                    bool isParsable = Int32.TryParse(choiceSearchStr, out choiceContactInt);
                    if (!isParsable)
                    {
                        Console.WriteLine($"'{choiceSearchStr}' is not a number.");
                        choiceContactInt = 0;
                    }

                    if(choiceContactInt>0 && choiceContactInt<=contacts.Count)
                    {
                        flagChooseContact = true;
                    }
                }while(flagChooseContact != true);

                // Options for manipulate contact
                choiceSearchStr = "";
                do{
                    int choiceInt = 0;
                    
                    Console.WriteLine("");
                    Console.WriteLine($"{choiceContactInt}.- {contactsList[choiceContactInt - 1].FirstName} {contactsList[choiceContactInt - 1].LastName.ToUpper()} {contactsList[choiceContactInt - 1].Email}");
                    Console.WriteLine("");
                    Console.WriteLine("What do want to do: ");

                    Console.WriteLine("1) \t Update");
                    Console.WriteLine("2) \t Delete");
                    Console.WriteLine($"{OPT_EXIT_KEY}) \t Exit");

                    Console.Write("#:");
                    choiceSearchStr = Console.ReadLine();

                    // Convert user choice to int and catch error
                    bool isParsable = Int32.TryParse(choiceSearchStr, out choiceInt);
                    if (!isParsable)
                    {
                        // Console.WriteLine($"'{choiceSearchStr}' is not a number.");
                        choiceInt = 0;
                    }

                    switch (choiceSearchStr)
                    {
                    case "1":
                        Console.WriteLine("");
                        Console.WriteLine("\t UPDATE");
                        Console.WriteLine("");
                        UpdateContact(contactsList[choiceContactInt - 1].Id);
                        break;
                    case "2":
                        Console.WriteLine("");
                        Console.WriteLine("\t DELETE");
                        Console.WriteLine("");
                        DeleteContact(contactsList[choiceContactInt - 1].Id);
                        break;
                    case OPT_EXIT_KEY:
                        Console.WriteLine(OPT_EXIT_VALUE);
                        break;
                    default:
                        Console.WriteLine($"'{choiceSearchStr}' is not an option.");
                        break;
                    }

                }while(choiceSearchStr != OPT_EXIT_KEY);
            }
        }
        else
        {
            Console.WriteLine("Invalid Input");
            throw new Exception("Invalid input.");
        }
    }

    public void UpdateContact(int contactID)
    {
        List<string> phoneObj = new List<string>();

        string? userInputStr = "";
        string? updatedFirstname = "";
        string? updatedLastname = "";
        string? updatedEmail = "";
        //
        ICollection<Phone> contactPhones = phoneService.GetPhonesByContactID(contactID);

        // Firstname
        do{
            Console.Write(LABEL_CREATE_NEW_FIRSTNAME +" : ");
            userInputStr = Console.ReadLine();
        }while(InputValidation.CheckFirstname(userInputStr) != true);

        updatedFirstname = InputValidation.FormatInputValue(userInputStr);

        // Lastname
        do
        {
            Console.Write(LABEL_CREATE_NEW_LASTNAME +" : ");
            userInputStr = Console.ReadLine();
        }while(InputValidation.CheckLastname(userInputStr) != true);

        updatedLastname = InputValidation.FormatInputValue(userInputStr);

        // Phone
        do
        {
            Console.Write(LABEL_CREATE_NEW_PHONE +" : ");
            userInputStr = Console.ReadLine();
            if(InputValidation.CheckPhone(userInputStr) == true)
            {
                phoneObj.Add(userInputStr);
            }
        }while(!string.IsNullOrEmpty(userInputStr));

        // Email
        do
        {
            Console.Write(LABEL_CREATE_NEW_EMAIL +" : ");
            userInputStr = Console.ReadLine();
        }while(InputValidation.CheckEmail(userInputStr) != true);

        updatedEmail = InputValidation.FormatInputValue(userInputStr);

        // Store in DB
        if(phoneObj.IsNullOrEmpty())
        {
            throw new Exception("Phone number cannot be empty");
        }
        else
        {
            Contact contact = new Contact
            {
                FirstName = updatedFirstname,
                LastName = updatedLastname,
                Email = updatedEmail
            };
            contact.Id = contactID;
            //
            contactService.Update(contact);

            // Delete before create new.
            foreach (var phone in contactPhones)
            {
                phoneService.Delete(phone);
            }

            foreach (var num in phoneObj)
            {
                phoneService.Create(new Phone { PhoneNumber = num, ContactId = contactID });
            }
        }
        
        // Console.WriteLine("Have been saved...");
        Console.WriteLine("{0}, {1} has been updated successfully.", 
        updatedFirstname, 
        updatedLastname);
    }

    public void DeleteContact(int contactID)
    {
        ICollection<Phone> contactPhones = phoneService.GetPhonesByContactID(contactID);
        Contact contact = contactService.GetById(contactID);

        if(contact != null)
        {
            string deletedFirstname = contact.FirstName;
            string deletedLastname = contact.LastName;

            if(!contactPhones.IsNullOrEmpty())
            {
                foreach (var phone in contactPhones)
                {
                    phoneService.Delete(phone);
                }
                //
                contactService.Delete(contact);
            }

            Console.WriteLine("{0}, {1} has been deleted successfully.", 
            deletedFirstname, 
            deletedLastname);
        }
        else
        {
            Console.WriteLine("Contct Not Found.");
        }
    }
}