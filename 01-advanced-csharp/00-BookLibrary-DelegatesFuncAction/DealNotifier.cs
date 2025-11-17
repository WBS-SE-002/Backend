public class DealNotifier(Func<PriceChangedEventArgs, bool> filter)
{
  private readonly Func<PriceChangedEventArgs, bool> _filter = filter;

  public void Subscribe(PricingService pricing) => pricing.PriceChanged += OnPriceChanged;
  public void OnPriceChanged(object? sender, PriceChangedEventArgs e)
  {
    if (!_filter(e)) return;
    Console.WriteLine($"[DEAL] Good news! '{e.Book.Title}' dropped from {e.OldPrice:C} to {e.NewPrice:C}");
  }
}