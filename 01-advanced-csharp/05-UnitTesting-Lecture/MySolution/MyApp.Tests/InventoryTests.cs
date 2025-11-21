using MyApp;

public class InventoryTests
{
  [Fact]
  public void AddAndGetAll_ContainsItems_InOrder()
  {
    var inv = new Inventory();
    inv.Add("apple");
    inv.Add("banana");

    var all = inv.GetAll();
    // for when Count should be 1
    // Assert.Single(all);
    // for when Count should be 0
    // Assert.Empty(all);
    Assert.Contains("apple", all);
    Assert.DoesNotContain("cherry", all);
    Assert.Collection(all,
        first => Assert.Equal("apple", first),
        second => Assert.Equal("banana", second));

    foreach (var item in all)
    {
      Assert.IsType<string>(item);
    }
  }
}