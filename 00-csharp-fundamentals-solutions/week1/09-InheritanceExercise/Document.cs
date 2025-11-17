public class Document(string title)
{
  public string Title { get; set; } = title;

  public virtual void PrintInfo()
  {
    Console.WriteLine($"Document: {Title}");
  }
}

public class Report(string title, string author) : Document(title)
{
  public string Author { get; set; } = author;

  public override void PrintInfo()
  {
    Console.WriteLine($"Report: {Title} from {Author}");
  }
}

public class Invoice(string title, decimal amount) : Document(title)
{
  public decimal Amount { get; set; } = amount;

  public override void PrintInfo()
  {
    Console.WriteLine($"Invoice: {Title} for {Amount:C}");
  }
}