namespace Generics.Services;

public class GenericList<T>
{
  private class Node(T t)
  {
    public T Data { get; set; } = t;
    public Node? Next { get; set; }
  }
  private Node? head;
  public void AddHead(T t)
  {
    Node n = new(t);
    n.Next = head;
    head = n;
  }

  public IEnumerator<T> GetEnumerator()
  {
    Node? current = head;

    while (current is not null)
    {
      yield return current.Data;
      current = current.Next;
    }
  }
}