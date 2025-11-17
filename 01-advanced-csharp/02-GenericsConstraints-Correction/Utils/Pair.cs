namespace GenericsExercise.Utils;

public class Pair<T1, T2>(T1 first, T2 second) : IEquatable<Pair<T1, T2>>
{
  public T1 First { get; set; } = first;
  public T2 Second { get; set; } = second;

  public void Deconstruct(out T1 first, out T2 second)
  {
    first = First;
    second = Second;
  }

  public bool Equals(Pair<T1, T2>? other)
  {
    if (other is null) return false;
    return EqualityComparer<T1>.Default.Equals(First, other.First)
        && EqualityComparer<T2>.Default.Equals(Second, other.Second);
  }

  public override bool Equals(object? obj) => Equals(obj as Pair<T1, T2>);

  public override int GetHashCode()
      => HashCode.Combine(
          EqualityComparer<T1>.Default.GetHashCode(First!),
          EqualityComparer<T2>.Default.GetHashCode(Second!)
      );

  public override string ToString() => $"({First}, {Second})";
}