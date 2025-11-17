namespace Services;

using Models;
public interface IProductInventoryService
{
  IReadOnlyCollection<Product> Products { get; }
  void Add(Product product);
  void Add(IEnumerable<Product> products);

  bool Remove(Guid id);
  Product? GetById(Guid id);
}