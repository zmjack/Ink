using Ink;
using NStandard;
using System;

namespace InkApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Echo.Left("Left").Center("Center").Right("Right").Line();
            Echo.CoverLeft("Left").CoverCenter("Center").CoverRight("Right").Line();

            string name = "";
            Echo.Ask("What's your name?", answer =>
            {
                if (answer.Value.IsNullOrWhiteSpace()) answer.Action = AskAction.Retry;
                else name = answer.Value = answer.Value.CapitalizeFirst();
            });
            Echo.Ask($"Mottos:{Environment.NewLine}", out var mottos, endsWith: Environment.NewLine);

            Echo.Line("Press ENTER to continue...")
                .PressContinue(ConsoleKey.Enter)
                .Line()
                .Line($"Hello, {name}!")
                .Line($"Your mottos are: {mottos.Replace(Environment.NewLine, " | ")}")
                .Line($"You can manage the following categories:")
                .Table(Categories);

            while (true)
            {
                Echo.AskYN("Exit? (Y/N) ", out var exit);
                if (exit) break;
            }
        }

        private class Category
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
            public string Description { get; set; }
        }

        private static Category[] Categories = new[]
        {
            new Category { CategoryID = 1, CategoryName = "Beverages", Description = "Soft drinks, coffees, teas, beers, and ales" },
            new Category { CategoryID = 2, CategoryName = "Condiments", Description = "Sweet and savory sauces, relishes, spreads, and seasonings" },
            new Category { CategoryID = 3, CategoryName = "Confections", Description = "Desserts, candies, and sweet breads" },
            new Category { CategoryID = 4, CategoryName = "Dairy Products", Description = "Cheeses" },
            new Category { CategoryID = 5, CategoryName = "Grains/Cereals", Description = "Breads, crackers, pasta, and cereal" },
            new Category { CategoryID = 6, CategoryName = "Meat/Poultry", Description = "Prepared meats" },
            new Category { CategoryID = 7, CategoryName = "Produce", Description = "Dried fruit and bean curd" },
            new Category { CategoryID = 8, CategoryName = "Seafood", Description = "Seaweed and fish" },
        };

    }
}
