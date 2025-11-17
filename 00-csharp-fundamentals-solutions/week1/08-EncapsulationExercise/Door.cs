public class Door
{
  public bool IsOpen { get; private set; }

  public void Open()
  {
    if (!IsOpen)
    {
      IsOpen = true;
      Console.WriteLine("Door opened.");
    }
  }

  public void Close()
  {
    if (IsOpen)
    {
      IsOpen = false;
      Console.WriteLine("Door closed.");
    }
  }

  public void Status()
  {
    Console.WriteLine(IsOpen ? "The door is open." : "The door is closed.");
  }
}