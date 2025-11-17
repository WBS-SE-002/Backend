namespace Utils;

public class Greeter
{
  public static void Welcome(string name = "student", string course = "C# Basics")
  {
    Console.WriteLine($"Welcome {name} to {course}");
  }
}