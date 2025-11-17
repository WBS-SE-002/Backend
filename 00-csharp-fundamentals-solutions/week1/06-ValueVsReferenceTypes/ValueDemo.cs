public class ValueDemo
{
  public static void Bump(int number)
  {
    number++;
    Console.WriteLine($"Increment received a copy of number and modified it: {number}");
  }
}