using Utils;

int sum = Calculator.Add(4, 5);

int product = Calculator.Multiply(7, 3);
Console.WriteLine($"sum: {sum}, product: {product}");


double doubleSum = Calculator.Add(4.5, 5.6);

double quotient = Calculator.Divide(23.78, 2.34);

double divideByZero = Calculator.Divide(34.0, 0);

Console.WriteLine($"doubleSum: {doubleSum}, quotient: {quotient}, divideByZero: {divideByZero}");

double avgOf2Nums = Statistics.Average(3, 5);
double avgOf3Nums = Statistics.Average(3, 5, 10);

Console.WriteLine($"avgOf2Nums: {avgOf2Nums}");
Console.WriteLine($"avgOf3Nums: {avgOf3Nums}");

string formattedInt = Formatter.FormatNumber(5);
string formattedDouble = Formatter.FormatNumber(5.0);
string formattedMessage = Formatter.FormatMessage("Help me Obi-Wan Kenobi", 5);

Console.WriteLine($"formattedInt: {formattedInt}");
Console.WriteLine($"formattedDouble: {formattedDouble}");
Console.WriteLine($"formattedMessage: {formattedMessage}");