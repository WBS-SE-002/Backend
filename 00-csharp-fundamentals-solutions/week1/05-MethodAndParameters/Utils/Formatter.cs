namespace Utils;

public class Formatter
{
  public static string FormatNumber(int n) => $"Number: {n}";

  // public static string FormatNumber(double n) => $"Double: {Math.Round(n, 2)}";
  public static string FormatNumber(double n) => $"Double: {n:0.00}";


  public static string FormatMessage(string text, int repeat = 1)
  {
    string result = "";

    for (int i = 0; i < repeat; i++)
    {
      result += text;
      if (i < repeat - 1) result += "\n";
    }
    return result;
  }
}