public class Shape()
{
  public virtual void Draw()
  {
    Console.WriteLine("Drew a shape...");
  }
}

public class Circle : Shape
{
  public void Draw()
  {
    Console.WriteLine("Drew a circle...");
  }
}
public class Square : Shape
{
  public override void Draw()
  {
    Console.WriteLine("Drew a square...");
  }
}