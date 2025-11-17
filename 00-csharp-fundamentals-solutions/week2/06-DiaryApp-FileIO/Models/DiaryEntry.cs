namespace DiaryApp.Models;

public class DiaryEntry(string title, string text)
{
  public string Title { get; } = title;
  public string Text { get; } = text;
  public DateTime Date { get; } = DateTime.Today;
}
