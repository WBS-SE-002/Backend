public sealed class BookAddedEventArgs(Book book, DateTimeOffset timestamp) : EventArgs
{
  public Book Book { get; } = book;
  public DateTimeOffset Timestamp { get; } = timestamp;
}
public sealed class BookRemovedEventArgs(string isbn, DateTimeOffset timestamp) : EventArgs
{
  public string Isbn { get; } = isbn;
  public DateTimeOffset Timestamp { get; } = timestamp;
}
public sealed class PriceChangedEventArgs(Book book, decimal oldPrice, decimal newPrice, DateTimeOffset timestamp) : EventArgs
{
  public Book Book { get; } = book;
  public decimal OldPrice { get; } = oldPrice;
  public decimal NewPrice { get; } = newPrice;
  public DateTimeOffset Timestamp { get; } = timestamp;
}


// public sealed class BookAddedEventArgs : EventArgs
// {
//   public Book Book { get; }
//   public DateTimeOffset Timestamp { get; }
//   public BookAddedEventArgs(Book book, DateTimeOffset timestamp)
//   {
//     Book = book;
//     Timestamp = timestamp;
//   }

// }