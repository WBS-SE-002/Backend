using Shop.Models;

namespace Shop.Services;

public class Order
{
  private readonly List<(Product Product, int Quantity)> _lines = new();

  public IReadOnlyCollection<(Product Product, int Quantity)> Lines => _lines.AsReadOnly();

  public void AddItem(Product product, int quantity)
  {
    // TODO: throw if product is null
    // if (product == null)
    // {
    //   throw new ArgumentNullException("Product can't be null");
    // }
    ArgumentNullException.ThrowIfNull(product);
    // TODO: throw ArgumentOutOfRangeException if quantity <= 0
    // if (quantity <= 0) throw new ArgumentOutOfRangeException("Quantity must be greater than 0");
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

    _lines.Add((product, quantity));
  }

  public decimal Total()
  {
    decimal sum = 0m;
    foreach (var (p, q) in _lines)
    {
      checked
      {
        sum += p.Price * q; // may overflow if you test extreme values
      }
    }
    return sum;
  }
}