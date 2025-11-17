
using ContactBook.Models;
using ContactBook.Services;
bool running = true;


while (running)
{
  Console.WriteLine("Choose an option:");
  Console.WriteLine("1. Add contact");
  Console.WriteLine("2. List contacts");
  Console.WriteLine("3. Get contact by name");
  Console.WriteLine("4. Remove contact by name");
  Console.WriteLine("5. Exit");
  Console.Write("Selection: ");

  var choice = Console.ReadLine();
  var contactService = new ContactService("contacts.json");
  switch (choice)
  {
    case "1":
      Console.Write("Enter name: ");
      string? name = Console.ReadLine();
      Console.Write("Enter email: ");
      string? email = Console.ReadLine();
      Console.Write("Enter phone: ");
      string? phone = Console.ReadLine();

      var newContact = new Contact(name!, email!, phone!);
      contactService.AddContact(newContact);
      break;

    case "2":
      contactService.ListContacts();

      break;
    case "3":
      Console.Write("Enter name to search: ");
      string? nameSearch = Console.ReadLine();
      contactService.GetByName(nameSearch!);
      break;

    case "4":
      Console.Write("Enter name to delete: ");
      string? nameDelete = Console.ReadLine();
      contactService.RemoveByName(nameDelete!);
      break;

    case "5":
      running = false;
      break;

    default:
      Console.WriteLine("Invalid selection");
      break;
  }

  Console.WriteLine();
}