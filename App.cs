public class App
{
  private static readonly Restaurant restaurant = new();
  private static readonly string[] foodCategories = ["салата", "супа", "основно ястие", "десерт", "напитка"];
  private static readonly ArgsHelper categoriesValidator = new(exact: 3);
  private static readonly ArgsHelper orderValidator = new(min: 1);
  private const StringComparison noCaps = StringComparison.CurrentCultureIgnoreCase;

  public static void Main()
  {
    while (true)
    {
      try
      {
        Printer.Info("> ");
        string? input = Console.ReadLine();
        if (input == null) Environment.Exit(1);
        string[] args = input.Split(",").Select(x => x.Trim()).ToArray();
        string cmd = args[0];
        args = args[1..];

        // New product command
        var cat = foodCategories.FirstOrDefault(x => x.StartsWith(cmd));
        if (cat != null)
        {
          if (!categoriesValidator.Validate(args)) throw new($"Трябват 3 (бяха {args.Length}) аргумента за тази команда");
          Product product = MakeMeal([.. args, cat]);
          restaurant.Menu.Add(product);
          continue;
        }

        // Order command
        if (int.TryParse(cmd, out int tableNumber))
        {
          if (!orderValidator.Validate(args)) throw new($"Трябват поне 2 (бяха {args.Length}) аргумента за тази команда");
          foreach (var mealName in args)
          {
            var meal = restaurant.GetProduct(mealName);
            if (meal != null) restaurant.Order(tableNumber, meal);
          }
          continue;
        }

        // Info command
        if (cmd.StartsWith("инфо", noCaps))
        {
          args = cmd.Split(' ')[1..];
          if (args.Length != 1) throw new("Трябва точно един аргумент за тази конманда");
          restaurant.GetProduct(args[0])?.PrintInfo();
          continue;
        }

        // Sales command
        if (cmd.Equals("продажби", noCaps))
        {
          restaurant.Print();
          continue;
        }

        // Exit command
        if (cmd.Equals("изход", noCaps))
        {
          restaurant.Print();
          Environment.Exit(0);
        }
      }
      catch (Exception e)
      {
        Printer.Errorln(e.Message);
      }
    }
  }

  private static Product MakeMeal(params string[] args)
  {
    if (!int.TryParse(args[1], out int mass)) throw new("Грамове трябва да бъдат цяло число");
    if (!decimal.TryParse(args[2], out decimal price)) throw new("Цената трябва да бъде реално число");
    return args[3] switch
    {
      "салата" => new Salad(args[0], mass, price, args[3]),
      "супа" => new Soup(args[0], mass, price, args[3]),
      "основно ястие" => new MainDish(args[0], mass, price, args[3]),
      "десерт" => new Dessert(args[0], mass, price, args[3]),
      "напитка" => new Drink(args[0], mass, price, args[3]),
      _ => throw new($"Невалидна категория: {args[3]}"),
    };
  }
}
