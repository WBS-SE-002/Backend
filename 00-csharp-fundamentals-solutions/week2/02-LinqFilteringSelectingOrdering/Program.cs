using Models;

var customers = new List<Customer>
{
    new() { Name = "Aisha",   City = "Berlin",  Age = 29, Account = new BankAccount { Iban = "DE01", Balance = 1800m, Currency = "EUR", Active = true } },
    new() { Name = "Jonas",   City = "Munich",  Age = 34, Account = new BankAccount { Iban = "DE02", Balance = 250m,  Currency = "EUR", Active = true } },
    new() { Name = "Priya",   City = "Berlin",  Age = 41, Account = new BankAccount { Iban = "DE03", Balance = 5200m, Currency = "EUR", Active = true } },
    new() { Name = "Luca",    City = "Hamburg", Age = 23, Account = new BankAccount { Iban = "DE04", Balance = 0m,    Currency = "EUR", Active = false } },
    new() { Name = "Noor",    City = "Leipzig", Age = 37, Account = new BankAccount { Iban = "DE05", Balance = 950m,  Currency = "EUR", Active = true } },
    new() { Name = "Omar",    City = "Berlin",  Age = 31, Account = new BankAccount { Iban = "DE06", Balance = 1400m, Currency = "EUR", Active = true } },
};

// Filtering
var activeInBerlin = customers.Where(customer => customer.City == "Berlin" && customer.Account.Active);

Console.WriteLine("Active customers in Berlin:");
foreach (var c in activeInBerlin)
{
    Console.WriteLine($"{c.Name}");
}

var aged25To40 = customers.Where(customer => customer.Age >= 25 && customer.Age <= 40);
Console.WriteLine("Customer Aged 25-40 (inclusive):");
foreach (var c in aged25To40)
{
    Console.WriteLine($"{c.Name}");
}

var highBalance = customers.Where(customer => customer.Account.Balance >= 1000);
Console.WriteLine("Customer with balance >= 1,000:");
foreach (var c in highBalance)
{
    Console.WriteLine($"{c.Name}");
}
var customersWithA = customers.Where(customer => customer.Name.ToLower().Contains('a'));
Console.WriteLine("Customer 'a' in their name:");
foreach (var c in customersWithA)
{
    Console.WriteLine($"{c.Name}");
}

// Selecting (projection)
var customerNames = customers.Select(c => c.Name);
Console.WriteLine("Customer names:");
foreach (var c in customerNames)
{
    Console.WriteLine(c);
}

var customersBasicInfo = customers.Select(c => new { c.Name, c.City, c.Account.Balance });
Console.WriteLine("Customer basic info (Name, City, Balance):");
foreach (var c in customersBasicInfo)
{
    Console.WriteLine($"{c.Name} {c.City} {c.Balance:C}");
}

var customerSummaries = customers.Select(c => new CustomerSummary(c.Name, c.City, c.Account.Balance, c.Account.Balance >= 1000));
Console.WriteLine("Customer summaries (Name, City, Balance, HighValue):");
foreach (var c in customerSummaries)
{
    Console.WriteLine($"{c.Name} {c.City} {c.Balance:C} HighValue: {c.HighValue}");
}

var byBalanceDescending = customers.OrderByDescending(c => c.Account.Balance);
Console.WriteLine("Customers by balance (descending):");
foreach (var c in byBalanceDescending)
{
    Console.WriteLine($"{c.Name} {c.Account.Balance:C} ");
}

var byCityThenName = customers
    .OrderBy(c => c.City)
    .ThenBy(c => c.Name);

Console.WriteLine("Customers ordered by city then name:");
foreach (var c in byCityThenName)
{
    Console.WriteLine($"{c.Name} {c.City}");
}

var byAge = customers.OrderBy(c => c.Age);
Console.WriteLine("Customers ordered by age:");
foreach (var c in byAge)
{
    Console.WriteLine($"{c.Name} {c.Age}");
}

// Stretch goal (combining)
var topActiveBerlin = customers
    .Where(customer => customer.City == "Berlin" && customer.Account.Active)
    .OrderByDescending(c => c.Account.Balance)
    .Select(c => new { c.Name, c.Account.Balance })
    .Take(3)
    .ToList();
Console.WriteLine("Top 3 Berlin:");
foreach (var c in topActiveBerlin)
{
    Console.WriteLine($"{c.Name} {c.Balance:C}");
}