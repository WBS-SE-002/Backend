namespace ContactBook.Services;

using ContactBook.Models;
using System.IO;
using System.Text.Json;

public class ContactService(string filePath)
{
  private readonly string _filePath = filePath;
  public void AddContact(Contact contact)
  {
    var currContacts = LoadContacts();

    currContacts.Add(contact);
    SaveContacts(currContacts);
    Console.WriteLine("Contact added.");

  }

  public void ListContacts()
  {
    var contacts = LoadContacts();

    foreach (var contact in contacts) Console.WriteLine($"{contact.Name}\n\t Email: {contact.Email}\n\t Phone: {contact.Phone}");
  }

  public void GetByName(string name)
  {
    var contacts = LoadContacts();
    Contact? searchResult = contacts.Find(c => c.Name == name);

    if (searchResult == null)
    {
      Console.WriteLine($"Could not find {name}");
    }
    else
    {
      Console.WriteLine($"{searchResult.Name}\n\t Email: {searchResult.Email}\n\t Phone: {searchResult.Phone}");
    }

  }

  public void RemoveByName(string name)
  {
    var contacts = LoadContacts();
    var updatedContacts = contacts.Where(c => c.Name != name).ToList();
    SaveContacts(updatedContacts);
    Console.WriteLine("Contact deleted");
  }
  private List<Contact> LoadContacts()
  {
    try
    {
      // if (!File.Exists(_filePath)) return new List<Contact>();
      if (!File.Exists(_filePath)) return [];

      string json = File.ReadAllText(_filePath);
      // return JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
      return JsonSerializer.Deserialize<List<Contact>>(json) ?? [];
    }
    catch (Exception ex) when (ex is IOException || ex is JsonException)
    {
      Console.WriteLine($"Error loading contacts: {ex.Message}");
      // return new List<Contact>();
      return [];
    }
  }

  private void SaveContacts(List<Contact> contacts)
  {
    try
    {
      var options = new JsonSerializerOptions { WriteIndented = true };
      string json = JsonSerializer.Serialize(contacts, options);
      File.WriteAllText(_filePath, json);
    }
    catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
    {
      Console.WriteLine($"Error saving contacts: {ex.Message}");
    }
  }
}