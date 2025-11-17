public class Calculator
{
  public static int Add(int a, int b) => a + b;

  public static double Add(double a, double b) => a + b;


  public static int Multiply(int a, int b) => a * b;


  public static double Divide(double a, double b)
  {
    if (b == 0) return double.NaN;

    return a / b;
  }
}