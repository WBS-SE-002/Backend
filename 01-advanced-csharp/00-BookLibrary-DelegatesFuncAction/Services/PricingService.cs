public class PricingService(CatalogService catalog)
{
  private readonly CatalogService _catalog = catalog;

  public event EventHandler<PriceChangedEventArgs>? PriceChanged;
  public void SetPrice(string isbn, decimal newPrice)
  {
    if (!_catalog.TryGet(isbn, out Book oldBook)) return;

    if (oldBook.Price == newPrice) return;

    Book updatedBook = oldBook with { Price = newPrice };
    _catalog.UpdateBook(updatedBook);
    OnPriceChange(new PriceChangedEventArgs(updatedBook, oldBook.Price, newPrice, DateTimeOffset.UtcNow));
  }

  protected virtual void OnPriceChange(PriceChangedEventArgs e) => PriceChanged?.Invoke(this, e);
}