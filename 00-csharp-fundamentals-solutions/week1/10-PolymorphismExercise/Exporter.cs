namespace FilesExporter;

public abstract class Exporter
{
  public string FileName { get; }

  protected Exporter(string fileName)
  {
    FileName = fileName;
  }

  public abstract void Export(string content);

  protected void Log(string kind)
  {
    Console.WriteLine($"[{kind}] -> {FileName}");
  }
}

public class PdfExporter : Exporter
{
  public PdfExporter(string fileName) : base(fileName) { }

  public override void Export(string content)
  {
    Log("PDF");
    Console.WriteLine($"Embedding text: {content}");
  }
}

public class CsvExporter : Exporter
{
  public CsvExporter(string fileName) : base(fileName) { }

  public override void Export(string content)
  {
    Log("CSV");
    Console.WriteLine($"Writing comma‑separated values: {content}");
  }
}

public class HtmlExporter : Exporter
{
  public HtmlExporter(string fileName) : base(fileName) { }

  public override void Export(string content)
  {
    Log("HTML");
    Console.WriteLine($"<p>{content}</p>");
  }
}