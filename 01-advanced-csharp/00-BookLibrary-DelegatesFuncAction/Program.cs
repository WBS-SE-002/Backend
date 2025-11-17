var catalog = new CatalogService();
var pricing = new PricingService(catalog);

var logger = new ConsoleLogger();
logger.Subscribe(catalog, pricing);

var notifier = new DealNotifier(e => e.NewPrice < e.OldPrice);
notifier.Subscribe(pricing);

var dune = new Book("9780441172719", "Dune", 20m);
var dune2 = new Book("9780441172719", "Dune", 20m);
var neu = new Book("9780441569595", "Neuromancer", 18m);

catalog.AddBook(dune);
catalog.AddBook(dune2);
catalog.AddBook(neu);

pricing.SetPrice(dune.Isbn, 17m); // drop → notifier reacts
pricing.SetPrice(neu.Isbn, 21m);  // increase → notifier ignores

pricing.PriceChanged -= logger.OnPriceChanged; // demonstrate unsubscribe
pricing.SetPrice(dune.Isbn, 16m); // logger should not log this change

catalog.RemoveBook(neu.Isbn);