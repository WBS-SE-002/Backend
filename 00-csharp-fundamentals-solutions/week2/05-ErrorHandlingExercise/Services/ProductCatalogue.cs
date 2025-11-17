using Shop.Models;

namespace Shop.Services;

public class ProductCatalogue
{
  private readonly Dictionary<string, Product> _bySku = new();

  public void AddProduct(Product product)
  {
    // TODO: throw if product is null (ArgumentNullException)
    // if (product == null)
    // {
    //   throw new ArgumentNullException("Product can't be null");
    // }
    ArgumentNullException.ThrowIfNull(product);
    // TODO: throw if SKU already exists (ArgumentException)
    if (_bySku.ContainsKey(product.Sku))
    {
      throw new ArgumentException("Sku already exists");
    }
    _bySku[product.Sku] = product;
  }

  public Product GetBySku(string sku)
  {
    // TODO: validate input and throw on null/empty sku
    if (string.IsNullOrEmpty(sku))
    {
      throw new ArgumentException("sku must be a non-empty string");
    }
    // TODO: throw KeyNotFoundException if not present
    if (!_bySku.TryGetValue(sku, out Product? value))
    {
      throw new KeyNotFoundException($"Product with sku {sku} not found");
    }
    return value;

    // if (!_bySku.ContainsKey(sku))
    // {
    //   throw new KeyNotFoundException($"Product with sku {sku} not found");
    // }
    // return _bySku[sku];
  }
}