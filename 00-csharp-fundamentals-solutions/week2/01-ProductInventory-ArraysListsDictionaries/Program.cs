using Models;
using Services;

var productInventoryService = new ProductInventoryService();

// Add a single product
var product1 = new Product { Name = "Laptop", Price = 999.99m, Stock = 10 };
productInventoryService.Add(product1);

// Add multiple products
List<Product> products =
[
    new() { Name = "Smartphone", Price = 499.99m, Stock = 25 },
    new() { Name = "Tablet", Price = 299.99m, Stock = 15 }
];
productInventoryService.Add(products);

// List all products
Console.WriteLine("All Products:");
foreach (var product in productInventoryService.Products)
{
  Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Stock: {product.Stock}");
}

// Get a product by ID
var retrievedProduct = productInventoryService.GetById(product1.Id);
if (retrievedProduct != null)
{
  Console.WriteLine($"\nRetrieved Product by ID {product1.Id}:");
  Console.WriteLine($"Name: {retrievedProduct.Name}, Price: {retrievedProduct.Price}, Stock: {retrievedProduct.Stock}");
}

// Remove a product by ID
var removeResult = productInventoryService.Remove(product1.Id);
Console.WriteLine($"\nProduct with ID {product1.Id} removed: {removeResult}");

// List all products after removal
Console.WriteLine("\nProducts after removal:");
foreach (var product in productInventoryService.Products)
{
  Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Stock: {product.Stock}");
}