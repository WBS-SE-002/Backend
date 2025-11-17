public class RefOps
{
  public static void Swap(ref int a, ref int b)
  {
    int temp = a;
    a = b;
    b = temp;
  }
}