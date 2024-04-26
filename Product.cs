abstract class Product(string name, int mass, decimal price, string cat)
{
  public string Name { get; } = name;
  public int Mass { get; } = mass;
  public decimal Price { get; set; } = price;
  public string Category { get; set; } = cat;
  public abstract void PrintInfo();
}

interface ICalories
{
  public double CaloriesModifier { get; }
  public double Calories { get; }
}

class Salad(string name, int mass, decimal price, string cat) : Product(name, mass, price, cat)
{
  public override void PrintInfo()
  {
    Printer.Infoln($"Информация за продукт: {Name}");
    Printer.Infoln($"Грамаж: {Mass}гр.");
    Printer.Infoln($"Цена: {Price}лв.");
  }
}

class Soup(string name, int mass, decimal price, string cat) : Product(name, mass, price, cat), ICalories
{
  public double CaloriesModifier { get; } = 1;
  public double Calories => Mass * CaloriesModifier;

  public override void PrintInfo()
  {
    Printer.Infoln($"Информация за продукт: {Name}");
    Printer.Infoln($"Грамаж: {Mass}гр.");
    Printer.Infoln($"Калории: {Calories}кал.");
    Printer.Infoln($"Цена: {Price}лв.");
  }
}

class MainDish(string name, int mass, decimal price, string cat) : Product(name, mass, price, cat), ICalories
{
  public double CaloriesModifier { get; } = 1.7;
  public double Calories => Mass * CaloriesModifier;

  public override void PrintInfo()
  {
    Printer.Infoln($"Информация за продукт: {Name}");
    Printer.Infoln($"Грамаж: {Mass}гр.");
    Printer.Infoln($"Калории: {Calories}кал.");
    Printer.Infoln($"Цена: {Price}лв.");
  }
}

class Dessert(string name, int mass, decimal price, string cat) : Product(name, mass, price, cat), ICalories
{
  public double CaloriesModifier { get; } = 3;
  public double Calories => Mass * CaloriesModifier;

  public override void PrintInfo()
  {
    Printer.Infoln($"Информация за продукт: {Name}");
    Printer.Infoln($"Грамаж: {Mass}гр.");
    Printer.Infoln($"Калории: {Calories}кал.");
    Printer.Infoln($"Цена: {Price}лв.");
  }
}

class Drink(string name, int mass, decimal price, string cat) : Product(name, mass, price, cat)
{
  public double CaloriesModifier { get; } = 1.2;
  public double Calories => Mass * CaloriesModifier;

  public override void PrintInfo()
  {
    Printer.Infoln($"Информация за продукт: {Name}");
    Printer.Infoln($"Милилитри: {Mass}мл.");
    Printer.Infoln($"Калории: {Calories}кал.");
    Printer.Infoln($"Цена: {Price}лв.");
  }
}
