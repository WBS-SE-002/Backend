namespace Shop.Models;

public class Product
{
  public string Sku { get; }
  public string Name { get; }
  public decimal Price { get; }

  public Product(string sku, string name, decimal price)
  {
    // TODO: validate inputs and throw
    // - sku/name must not be null or whitespace (ArgumentException/ArgumentNullException)
    if (string.IsNullOrWhiteSpace(sku))
    {
      throw new ArgumentException("sku must be a non-empty string");
    }

    if (string.IsNullOrWhiteSpace(name))
    {
      throw new ArgumentException("name must be a non-empty string");
    }

    // - price must be > 0 (ArgumentOutOfRangeException)
    // if (price <= 0)
    // {
    //   throw new ArgumentOutOfRangeException("price must be greater than 0");
    // }
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
    Sku = sku;
    Name = name;
    Price = price;
  }
}