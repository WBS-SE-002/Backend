public class ConsoleLogger
{
  public void Subscribe(CatalogService catalog, PricingService pricing)
  {
    catalog.BookAdded += OnBookAdded;
    catalog.BookRemoved += OnBookRemoved;
    pricing.PriceChanged += OnPriceChanged;
  }
  public void Unsubscribe(CatalogService catalog, PricingService pricing)
  {
    catalog.BookAdded -= OnBookAdded;
    catalog.BookRemoved -= OnBookRemoved;
    pricing.PriceChanged -= OnPriceChanged;
  }

  public void OnBookAdded(object? sender, BookAddedEventArgs e)
        => Console.WriteLine($"[LOG] Added: {e.Book.Title} at {e.Timestamp:t}");

  public void OnBookRemoved(object? sender, BookRemovedEventArgs e)
      => Console.WriteLine($"[LOG] Removed ISBN: {e.Isbn} at {e.Timestamp:t}");

  public void OnPriceChanged(object? sender, PriceChangedEventArgs e)
      => Console.WriteLine($"[LOG] Price change: {e.Book.Title} {e.OldPrice:C} → {e.NewPrice:C}");
}