public class Statistics
{
  public static double Average(double a, double b, double c = 0)
  {
    double sum = a + b + c;

    if (c == 0) return sum / 2;

    return sum / 3;
  }
}