namespace _
{
  class Meal(string name, int mass, decimal price, string cat)
  {
    private readonly static Dictionary<string, double> caloriesMod = new()
    {
      ["салата"] = 0,
      ["супа"] = 1,
      ["основно ястие"] = 1.7,
      ["десерт"] = 3,
      ["напитка"] = 1.2,
    };
    public string Name { get; set; } = name;
    public int Mass
    {
      get => mass;
      set
      {
        if (value > 1000 || value < 0)
          throw new($"{(Drink ? "Милилитри" : "Грамаж")} трябва да е между 0-1000");
        mass = value;
      }
    }
    public decimal Price { get; set; } = price;
    public string Category { get; set; } = cat;

    protected double CaloriesModifier { get; private set; } = caloriesMod[cat];
    public double Calories { get => Mass * CaloriesModifier; }

    public bool Drink { get; } = cat == "напитка";

    public override string ToString() => $"{Name}: {Mass}{(Drink ? "мл" : "гр")}. {Price}лв.{(Calories > 0 ? "" : $" {Calories}кал.")}";
  }
}
