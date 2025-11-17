var books = new List<Book>();

string choice = "";

while (choice != "5")
{
  Console.WriteLine();
  Console.WriteLine("Library Menu");
  Console.WriteLine("1) Add Book");
  Console.WriteLine("2) Get Book");
  Console.WriteLine("3) Edit Book");
  Console.WriteLine("4) Remove Book");
  Console.WriteLine("5) Exit");

  choice = ReadNonEmpty("Choose an option (1-5): ").ToLower();

  switch (choice)
  {
    case "1":
      AddBook();
      break;
    case "2":
      GetBook();
      break;
    case "3":
      EditBook();
      break;
    case "4":
      RemoveBook();
      break;
    case "5":
      Console.WriteLine("Goodbye!");
      break;
    default:
      Console.WriteLine("Invalid option. Please choose 1-5.");
      break;
  }
}

string ReadNonEmpty(string prompt = "")
{
  while (true)
  {
    Console.Write(prompt);
    string? input = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(input))
      return input.Trim();

    Console.WriteLine("Please enter a non-empty value.");
  }
}

int ReadInt(string prompt = "")
{
  while (true)
  {
    Console.Write(prompt);
    string? input = Console.ReadLine();

    if (int.TryParse(input, out int n))
      return n;

    Console.WriteLine("Please enter a valid integer.");
  }
}

Book? FindByTitle(string title) => books.Find(item => string.Equals(item.Title, title, StringComparison.OrdinalIgnoreCase));


void AddBook()
{
  string title = ReadNonEmpty("Enter the title: ");
  string author = ReadNonEmpty("Enter the author: ");
  int year = ReadInt("Enter year written: ");


  books.Add(new Book(title, author, year));
  Console.WriteLine("Book added.");
}

void GetBook()
{
  string title = ReadNonEmpty("Enter title of book: ");

  var foundBook = FindByTitle(title);

  if (foundBook == null)
  {
    Console.WriteLine($"Sorry, {title} wasn't found");
  }
  else
  {
    foundBook.DisplayInfo();
  }

}

void EditBook()
{
  string title = ReadNonEmpty("Enter title of book: ");

  var foundBook = FindByTitle(title);

  if (foundBook == null)
  {
    Console.WriteLine($"Sorry, {title} wasn't found");
  }
  else
  {
    string newTitle = ReadNonEmpty("Enter new title: ");
    foundBook.Title = newTitle;
    Console.WriteLine("Book updated.");
  }
}
void RemoveBook()
{
  string title = ReadNonEmpty("Enter title of book: ");

  var foundBook = FindByTitle(title);

  if (foundBook == null)
  {
    Console.WriteLine($"Sorry, {title} wasn't found");
  }
  else
  {
    books.Remove(foundBook);
    Console.WriteLine("Book removed.");
  }
}