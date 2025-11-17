public class CatalogService
{
  private readonly Dictionary<string, Book> _books = new();
  public event EventHandler<BookAddedEventArgs>? BookAdded;
  public event EventHandler<BookRemovedEventArgs>? BookRemoved;

  public void AddBook(Book book)
  {
    if (_books.ContainsKey(book.Isbn)) return;
    _books[book.Isbn] = book;
    OnBookAdded(new BookAddedEventArgs(book, DateTimeOffset.UtcNow));
  }
  public void RemoveBook(string isbn)
  {
    if (!TryGet(isbn, out Book bookToRemove)) return;
    _books.Remove(isbn);
    OnBookRemoved(new BookRemovedEventArgs(isbn, DateTimeOffset.UtcNow));
  }
  public bool TryGet(string isbn, out Book book) => _books.TryGetValue(isbn, out book!);

  public bool UpdateBook(Book updated)
  {
    if (!_books.ContainsKey(updated.Isbn)) return false;
    _books[updated.Isbn] = updated;
    return true;
  }
  protected virtual void OnBookAdded(BookAddedEventArgs e) => BookAdded?.Invoke(this, e);
  protected virtual void OnBookRemoved(BookRemovedEventArgs e) => BookRemoved?.Invoke(this, e);
}