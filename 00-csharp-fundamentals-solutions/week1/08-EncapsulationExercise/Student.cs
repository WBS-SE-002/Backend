public class Student
{
  public string Name { get; set; }
  public int Grade { get; private set; }

  public Student(string name = "John Doe", int grade = 100)
  {
    Name = name;
    Grade = grade;
  }

  public void UpdateGrade(int newGrade)
  {
    if (newGrade >= 0 && newGrade <= 100)
    {
      Grade = newGrade;
    }
  }

  public void Introduce()
  {
    Console.WriteLine($"I'm {Name} and my grade is {Grade}");
  }

}

// public class Student
// {
//   public string Name { get; set; } = string.Empty;
//   public int Grade { get; private set; }

//   public void UpdateGrade(int newGrade)
//   {
//     if (newGrade < 0 || newGrade > 100)
//     {
//       Console.WriteLine("Grade must be 0..100. Ignored.");
//       return;
//     }
//     Grade = newGrade;
//   }

//   public void Introduce()
//   {
//     Console.WriteLine($"Hi, I'm {Name} and my grade is {Grade}.");
//   }
// }