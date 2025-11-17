using DiaryApp.Models;
using DiaryApp.Services;

bool running = true;

while (running)
{
  Console.WriteLine("Choose an option:");
  Console.WriteLine("1. Log entry");
  Console.WriteLine("2. Retrieve entry");
  Console.WriteLine("3. Exit");
  Console.Write("Selection: ");

  var choice = Console.ReadLine();
  var diary = new DiaryService();
  switch (choice)
  {
    case "1":
      Console.Write("Enter title: ");
      string? title = Console.ReadLine();
      Console.Write("Enter text: ");
      string? text = Console.ReadLine();
      // TODO: use DiaryWriter to save entry
      var newEntry = new DiaryEntry(title!, text!);
      diary.SaveEntry(newEntry);
      break;

    case "2":
      Console.Write("Enter date (yyyy-mm-dd): ");
      string? date = Console.ReadLine();
      Console.Write("Enter title: ");
      string? readTitle = Console.ReadLine();
      // TODO: use DiaryReader to retrieve entry
      string? entryText = diary.ReadEntry(date!, readTitle!);

      if (entryText != null) Console.WriteLine(entryText);

      break;

    case "3":
      running = false;
      break;

    default:
      Console.WriteLine("Invalid selection");
      break;
  }

  Console.WriteLine();
}