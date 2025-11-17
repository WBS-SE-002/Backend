var document = new Document("What's up doc?");
var report = new Report("Reported", "You know who");
var invoice = new Invoice("Get out of my voice!", 133.40M);

document.PrintInfo();
report.PrintInfo();
invoice.PrintInfo();

var shape = new Shape();
var circle = new Circle();
var square = new Square();

shape.Draw();
circle.Draw();
square.Draw();

Shape demoCircle = new Circle();
demoCircle.Draw();

Circle derivedCircle = (Circle)demoCircle;

derivedCircle.Draw();

// Membership hierarchy
Membership m1 = new Membership("Lina");
Membership m2 = new StandardMembership("Jonas");
Membership m3 = new PremiumMembership("Aisha");
Membership m4 = new LifetimeMembership("Omar");

foreach (var m in new[] { m1, m2, m3, m4 })
{
  Console.WriteLine($"{m.MemberName}: {m.GetBenefits()}");
}