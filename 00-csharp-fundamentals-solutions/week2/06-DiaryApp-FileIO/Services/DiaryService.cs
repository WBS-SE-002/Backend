namespace DiaryApp.Services;

using DiaryApp.Models;
using System.IO;
public class DiaryService
{
  public string RootDirectory = "DiaryEntries";
  public void SaveEntry(DiaryEntry entry)
  {
    try
    {
      string dateString = entry.Date.ToString("yyyy-MM-dd");
      string dateFolder = Path.Combine(RootDirectory, dateString);

      if (!Directory.Exists(dateFolder))
      {
        Directory.CreateDirectory(dateFolder);
      }
      // simple version
      // string fullPath = Path.Combine(dateFolder, entry.Title + ".txt");

      // File.WriteAllText(fullPath, entry.Text);

      string safeTitle = string.Join("_", entry.Title.Split(Path.GetInvalidFileNameChars()));
      string filePath = Path.Combine(dateFolder, safeTitle + ".txt");

      using (var writer = new StreamWriter(filePath, append: true))
      {
        writer.WriteLine($"[{DateTime.Now:HH:mm}] {entry.Text}");
      }
      Console.WriteLine("Entry saved successfully.");
    }
    catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
    {
      Console.WriteLine($"Error saving entry: {ex.Message}");
    }

  }

  public string? ReadEntry(string date, string title)
  {
    try
    {

      string dateFolder = Path.Combine(RootDirectory, date);
      string safeTitle = string.Join("_", title.Split(Path.GetInvalidFileNameChars()));
      string fullPath = Path.Combine(dateFolder, safeTitle + ".txt");

      if (!File.Exists(fullPath))
      {
        Console.WriteLine($"File {title} from day {date} not found");
        return null;
      }


      string text = File.ReadAllText(fullPath);
      // Console.WriteLine(text);
      return text;
    }
    catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
    {
      Console.WriteLine($"Error reading entry: {ex.Message}");
      return null;
    }
  }
}