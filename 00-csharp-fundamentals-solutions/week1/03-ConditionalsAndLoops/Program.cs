Console.Write("Enter your age: ");
string ageInput = Console.ReadLine()!;
if (!int.TryParse(ageInput, out int age))
{
  Console.WriteLine("Invalid age. Using default age = 20.");
  age = 20;
}

if (age < 13)
{
  Console.WriteLine("You are a child");
}
else if (age < 20)
{
  Console.WriteLine("You are a teenager.");

}
else
{
  Console.WriteLine("You are an adult.");
}

string ageGroup = age < 13 ? "a child" : age < 20 ? " a teenager" : "an adult.";
Console.WriteLine($"(Ternary) You are {ageGroup}.");

Console.Write("Enter your grade: ");

string? grade = Console.ReadLine()!.ToUpper();

switch (grade)
{
  case "A":
    Console.WriteLine("Excellent");
    break;
  case "B":
    Console.WriteLine("Good");
    break;
  case "C":
    Console.WriteLine("Average");
    break;
  case "D":
    Console.WriteLine("Below average");
    break;
  case "F":
    Console.WriteLine("Fail");
    break;
  default:
    Console.WriteLine("Unknown grade");
    break;
}

// While Loop – sum 1..100
int sum = 0;
int i = 1;
while (i <= 100)
{
  sum += i;
  i++;
}
Console.WriteLine($"Sum 1..100 = {sum}");

// Do..While – guessing game
int target = 7; // hard-coded target
int guess;
do
{
  Console.Write("Guess a number between 1 and 10: ");
  string input = Console.ReadLine()!;
  if (!int.TryParse(input, out guess))
  {
    Console.WriteLine("Please enter digits only.");
    guess = -1;
    continue;
  }

  if (guess < target)
  {
    Console.WriteLine("Too low, try again.");
  }
  else if (guess > target)
  {
    Console.WriteLine("Too high, try again.");
  }
}
while (guess != target);
Console.WriteLine("Correct! 🎉");

// For Loop – 7 times table
for (int n = 1; n <= 10; n++)
{
  Console.WriteLine($"7 × {n} = {7 * n}");
}

// Break and Continue – 1..20, skip evens, stop at 15
for (int k = 1; k <= 20; k++)
{
  if (k % 2 == 0) continue;
  if (k == 15) break;
  Console.Write(k + " ");
}
Console.WriteLine();