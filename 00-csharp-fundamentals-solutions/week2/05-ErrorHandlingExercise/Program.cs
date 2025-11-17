using Shop.Models;
using Shop.Services;
using Shop.Payments;

var catalogue = new ProductCatalogue();
var processor = new PaymentProcessor();

// Happy-path product
var valid = new Product("ABC-123", "Coffee Mug", 12.50m);

// TODO: uncomment once you implement throws in Product constructor
try
{
  // var badName = new Product("XYZ-999", "", 5m);            // should throw
  var badPrice = new Product("BAD-000", "Napkin", 0m);      // should throw
  // After implementing, either comment them back again or handle exceptions!
}
catch (Exception ex)
{
  Console.WriteLine($"Error: {ex.Message}");
}

// Add to catalogue
catalogue.AddProduct(valid);

// TODO: uncomment to test duplicate SKU handling
try
{

  catalogue.AddProduct(new Product("ABC-123", "Duplicate", 1m)); // should throw
}
catch (Exception ex)
{

  Console.WriteLine($"Error: {ex.Message}");
}

// Lookup and order
var order = new Order();
var fetched = catalogue.GetBySku("ABC-123");
order.AddItem(fetched, 2);

Console.WriteLine($"Order total: {order.Total():0.00}");

// Intentionally provoke failures and HANDLE them here
try
{
  // Missing SKU
  var missing = catalogue.GetBySku("DOES-NOT-EXIST");
}
catch (KeyNotFoundException ex)
{
  Console.WriteLine($"Catalogue error: {ex.Message}");
}

try
{
  // Invalid quantity
  order.AddItem(fetched, 0);
}
catch (ArgumentOutOfRangeException ex)
{
  Console.WriteLine($"Order error: {ex.Message}");
}

try
{
  // Payment scenarios
  processor.Charge("0000-1111-2222-3333", order.Total());       // likely to be declined
  processor.Charge("4444-5555-6666-7777", -10m);                 // invalid amount
}
catch (PaymentDeclinedException ex)
{
  Console.WriteLine($"Payment declined: {ex.Reason} (Amount={ex.Amount})");
}
catch (ArgumentException ex)
{
  Console.WriteLine($"Bad argument: {ex.Message}");
}
catch (Exception ex)
{
  // last-resort catch to avoid crashing the app in the exercise
  Console.WriteLine($"Unexpected error: {ex.Message}");
}
finally
{
  Console.WriteLine("End of payment attempt.");
}

Console.WriteLine("Done.");