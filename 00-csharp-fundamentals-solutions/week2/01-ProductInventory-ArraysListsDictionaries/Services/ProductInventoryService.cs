namespace Services;

using Models;
public class ProductInventoryService : IProductInventoryService
{
  private readonly ICollection<Product> _products = [];

  public IReadOnlyCollection<Product> Products => (IReadOnlyCollection<Product>)_products;

  public void Add(Product newProduct)
  {
    _products.Add(newProduct);
  }
  public void Add(IEnumerable<Product> newProducts)
  {
    foreach (var newProduct in newProducts)
    {
      _products.Add(newProduct);
    }
  }
  public bool Remove(Guid productId)
  {
    Product? toRemove = null;
    foreach (var product in _products)
    {
      if (product.Id == productId)
      {
        toRemove = product;
        break;
      }
    }
    return toRemove != null && _products.Remove(toRemove);
  }
  public Product? GetById(Guid productId)
  {
    Product? matchingProduct = null;
    foreach (var product in _products)
    {
      if (product.Id == productId)
      {
        matchingProduct = product;
        break;
      }
    }
    return matchingProduct;
  }
}