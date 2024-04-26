class Restaurant
{
  public List<Product> Menu = [];
  public Dictionary<int, List<TableEntry>> Tables = [];

  public Product? GetProduct(string name)
  {
    if (name == "") return null;
    return Menu.Find(meal => meal.Name.StartsWith(name, StringComparison.CurrentCultureIgnoreCase));
  }

  public void Order(int table, Product product)
  {
    if (Tables.TryGetValue(table, out List<TableEntry>? entries))
    {
      var entry = entries.Find(entry => entry.Product.Equals(product));
      if (entry != null) entry.Increment();
      else entries.Add(new(product));
    }
    else
      Tables.Add(table, [new(product)]);
  }

  public void Print()
  {
    Printer.Infoln($"Общо заети маси през деня: {Tables.Count}");
    var priceAll = Tables.Select(x => x.Value.Sum(x => x.Count * x.Product.Price)).Sum();
    Printer.Infoln($"Общо продажби: {Tables.Select(x => x.Value.Sum(x => x.Count)).Sum()} - {priceAll}");
    Printer.Infoln("По категории:");

    string[] categories = ["салата", "супа", "основно ястие", "десерт", "напитка"];
    foreach (var category in categories)
    {
      var items = Tables.Select(x => x.Value.Where(x => x.Product.Category == category)).SelectMany(x => x).ToList();
      var price = items.Select(x => x.Count * x.Product.Price).Sum();

      Printer.Infoln($"\t{category}: {items.Select(x => x.Count).Sum()} - {price}лв.");
    }
  }

  public class TableEntry(Product product)
  {
    public Product Product { get; private set; } = product;
    public int Count { get; private set; } = 1;

    public void Increment()
    {
      Count++;
    }
  }
}