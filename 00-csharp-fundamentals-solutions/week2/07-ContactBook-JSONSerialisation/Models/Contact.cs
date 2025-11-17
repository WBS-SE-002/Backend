namespace ContactBook.Models;

public class Contact(string name, string email, string phone)
{
  public string Name { get; set; } = name;
  public string Email { get; set; } = email;
  public string Phone { get; set; } = phone;
}