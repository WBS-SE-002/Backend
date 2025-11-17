namespace Game;

public class GameCharacter(string name, int level)
{
  public string Name { get; set; } = name;
  public int Level { get; set; } = level;

  public virtual void UseSpecial() => Console.WriteLine("Did the thing!");

  public virtual void Describe() => Console.WriteLine($"I'm {Name}! A level {Level} character.");
}

public class Warrior(string name, int level) : GameCharacter(name, level)
{
  public override void UseSpecial() => Console.WriteLine("Shield bash!");

  public override void Describe() => Console.WriteLine($"I'm {Name}! A level {Level} Warrior.");
}
public class Mage(string name, int level) : GameCharacter(name, level)
{
  public override void UseSpecial() => Console.WriteLine("Arcane burst!");

  public override void Describe() => Console.WriteLine($"I'm {Name}! A level {Level} Mage.");
}
public class Healer(string name, int level) : GameCharacter(name, level)
{
  public override void UseSpecial() => Console.WriteLine("Group heal!");

  public override void Describe() => Console.WriteLine($"I'm {Name}! A level {Level} Healer.");
}