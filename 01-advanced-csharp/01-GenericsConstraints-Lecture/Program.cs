
using Generics.Services;
using Generics.Models;

// List<string> myStrings = new List<string> { "hi", "yes", "no", "why" };
// List<string> myStrings = new() { "hi", "yes", "no", "why" };
// List<string> myStrings = ["hi", "yes", "no", "why"];
var myStrings = new List<string> { "hi", "yes", "no", "why" };

Dictionary<string, string> phoneBook = new()
{
  ["Jeff"] = "0123456789"
};

Func<decimal, decimal, decimal> divide = (n1, n2) => n1 / n2;

Console.WriteLine(divide(20m, 7m));

int? nullableInt = 5;
// Nullable<int> nullableInt = 5;
nullableInt = null;
nullableInt = 7;

GenericList<string> stringList = new();

stringList.AddHead("C#");
stringList.AddHead("from");
stringList.AddHead("World");
stringList.AddHead("Hello");

foreach (var s in stringList)
{
  Console.WriteLine(s);
}

int a = 1, b = 2;
Swap(ref a, ref b);
Console.WriteLine($"a = {a}, b = {b}"); // a = 2, b = 1

string x = "hello", y = "world";
Swap(ref x, ref y);
Console.WriteLine($"x = {x}, y = {y}"); // x = world, y = hello

void Swap<T>(ref T lhs, ref T rhs)
{
  (lhs, rhs) = (rhs, lhs);
}

Console.WriteLine("== GenericRepository<T> demo ==");
Console.WriteLine("== Employees ==");
var empRepo = new GenericRepository<Employee>();
empRepo.Add(new Employee { Id = 1, Name = "Ada", Department = "R&D" });
empRepo.Add(new Employee { Id = 2, Name = "Grace", Department = "Ops" });

var e = empRepo.GetById(1);
Console.WriteLine($"GetById(1): {e}");
Console.WriteLine();

Console.WriteLine("== BlogPosts ==");
var blogRepo = new GenericRepository<BlogPost>();
blogRepo.Add(new BlogPost { Id = 1, Title = "Generics in C#", Likes = 10 });
blogRepo.Add(new BlogPost { Id = 2, Title = "Understanding Delegates", Likes = 25 });

var post = blogRepo.GetById(2);
Console.WriteLine($"GetById(2): {post}");