public class Book
{

  public Book(string title, string author, int year)
  {
    Title = title;
    Author = author;
    Year = year;
  }
  public string Title { get; set; }
  public string Author { get; set; }
  public int Year { get; set; }

  public void DisplayInfo()
  {
    // Console.WriteLine($"Title: {Title}, Author: {Author}, Year: {Year}");
    Console.WriteLine($"\"{Title}\" by {Author} ({Year})");
  }
}