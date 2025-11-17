var employees = new List<Employee>
{
    new Employee { Id = 1, Name = "Aisha", Department = "Engineering" },
    new Employee { Id = 2, Name = "Jonas", Department = "Design" },
    new Employee { Id = 3, Name = "Priya", Department = "Engineering" },
    new Employee { Id = 4, Name = "Luca", Department = "Marketing" },
};

var assignments = new List<ProjectAssignment>
{
    new ProjectAssignment { EmployeeId = 1, Project = "Website Redesign" },
    new ProjectAssignment { EmployeeId = 1, Project = "Payment Gateway" },
    new ProjectAssignment { EmployeeId = 2, Project = "Website Redesign" },
    new ProjectAssignment { EmployeeId = 3, Project = "Data Migration" },
    new ProjectAssignment { EmployeeId = 4, Project = "Social Media Campaign" },
};

// Join
var assignedEmployees = employees.Join(assignments,
employee => employee.Id,
assignment => assignment.EmployeeId,
(employee, assignment) => new { employee.Name, assignment.Project });

Console.WriteLine("Employees on project:");
foreach (var item in assignedEmployees) Console.WriteLine($"{item.Name} -> {item.Project}");
Console.WriteLine();

var assignedEmployeesQuery = from e in employees
                             join a in assignments
                             on e.Id equals a.EmployeeId
                             select new { e.Name, a.Project };

Console.WriteLine("Employees on project (query):");
foreach (var item in assignedEmployeesQuery) Console.WriteLine($"{item.Name} -> {item.Project}");
Console.WriteLine();


// GroupBy
var employeesPerDepartment = employees.GroupBy(e => e.Department);
foreach (var dep in employeesPerDepartment)
{
  Console.WriteLine($"{dep.Key} Department:");
  foreach (var emp in dep) Console.WriteLine($"{emp.Name}");
  Console.WriteLine();
}
Console.WriteLine();

var assignmentsPerProject = assignments.GroupBy(a => a.Project);
foreach (var proj in assignmentsPerProject)
{
  Console.WriteLine($"{proj.Key} Project:");
  foreach (var assn in proj) Console.WriteLine($"Employee Id: {assn.EmployeeId}");
  Console.WriteLine();
}
Console.WriteLine();

// GroupJoin
var employeesWithProjects = employees.GroupJoin(assignments,
                                                e => e.Id,
                                                a => a.EmployeeId,
(e, a) => new { e.Name, Assignments = a });
foreach (var emp in employeesWithProjects)
{
  Console.WriteLine($"{emp.Name} Assignments:");
  foreach (var assn in emp.Assignments) Console.WriteLine($"{assn.Project}");
  Console.WriteLine();
}
Console.WriteLine();

// -------------------
// Stretch goals
// -------------------
Console.WriteLine();
Console.WriteLine("-- Count employees per department --");
var deptCounts = employees.GroupBy(e => e.Department)
                          .Select(g => new { Department = g.Key, Count = g.Count() });
foreach (var d in deptCounts)
{
  Console.WriteLine($"{d.Department}: {d.Count}");
}

Console.WriteLine();
Console.WriteLine("-- Projects with employee names --");
var projectEmployees = assignments.Join(
    employees,
    a => a.EmployeeId,
    e => e.Id,
    (a, e) => new { a.Project, e.Name })
    .GroupBy(x => x.Project);

foreach (var g in projectEmployees)
{
  Console.WriteLine(g.Key);
  foreach (var x in g)
  {
    Console.WriteLine($"  {x.Name}");
  }
}