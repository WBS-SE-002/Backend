using Game;
using FilesExporter;
using Transport;

var characters = new List<GameCharacter>
{
  new Warrior("Argus", 7),
  new Mage("Mageaegicus", 8),
  new Healer("McHealer", 9)
};

foreach (var character in characters)
{
  character.Describe();
  character.UseSpecial();
}

// Exporter demo
var exporters = new List<Exporter>
{
    new PdfExporter("report.pdf"),
    new CsvExporter("data.csv"),
    new HtmlExporter("page.html")
};

Console.WriteLine();
foreach (var ex in exporters)
{
  ex.Export("Sample content");
}

Console.WriteLine();

// AccessPass demo
var passes = new List<AccessPass>
{
    new MetroPass("Amina", zones: 2),
    new BikeSharePass("Jonas", minutes: 15),
    new FerryPass("Luca", DateTime.UtcNow.AddDays(3))
};

foreach (var p in passes)
{
  bool valid = p.Validate();
  Console.WriteLine($"Valid -> {valid}");
}
