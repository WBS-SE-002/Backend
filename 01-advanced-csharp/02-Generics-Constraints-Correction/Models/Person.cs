namespace GenericsExercise.Models;

sealed class Person : IComparable<Person>
{
  public string Name { get; init; } = "";
  public int Age { get; init; }

  public int CompareTo(Person? other)
      => other is null ? 1 : Age.CompareTo(other.Age);

  public override string ToString() => $"{Name} ({Age})";
}