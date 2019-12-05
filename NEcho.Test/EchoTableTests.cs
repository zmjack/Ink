using System;
using Xunit;

namespace NEcho.Test
{
    public class EchoTableTests
    {
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

        [Fact]
        public void BorderTableTest()
        {
            Assert.Equal(
@"+------------+----------------+------------------------------------------------------------+
| CategoryID | CategoryName   | Description                                                |
+------------+----------------+------------------------------------------------------------+
| 1          | Beverages      | Soft drinks, coffees, teas, beers, and ales                |
| 2          | Condiments     | Sweet and savory sauces, relishes, spreads, and seasonings |
| 3          | Confections    | Desserts, candies, and sweet breads                        |
| 4          | Dairy Products | Cheeses                                                    |
| 5          | Grains/Cereals | Breads, crackers, pasta, and cereal                        |
| 6          | Meat/Poultry   | Prepared meats                                             |
| 7          | Produce        | Dried fruit and bean curd                                  |
| 8          | Seafood        | Seaweed and fish                                           |
+------------+----------------+------------------------------------------------------------+
", ConUtility.BorderTable(Categories));
        }

        [Fact]
        public void NoBorderTableTest()
        {
            Assert.Equal(
@"CategoryID CategoryName   Description                                               
1          Beverages      Soft drinks, coffees, teas, beers, and ales               
2          Condiments     Sweet and savory sauces, relishes, spreads, and seasonings
3          Confections    Desserts, candies, and sweet breads                       
4          Dairy Products Cheeses                                                   
5          Grains/Cereals Breads, crackers, pasta, and cereal                       
6          Meat/Poultry   Prepared meats                                            
7          Produce        Dried fruit and bean curd                                 
8          Seafood        Seaweed and fish                                          
", ConUtility.NoBorderTable(Categories));
        }

        [Fact]
        public void CreateSeamlessTableTest()
        {
            Assert.Equal(
@"©°©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©Ð©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©Ð©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©´
©¦ CategoryID©¦ CategoryName  ©¦ Description                                               ©¦
©À©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©à©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©à©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©È
©¦ 1         ©¦ Beverages     ©¦ Soft drinks, coffees, teas, beers, and ales               ©¦
©¦ 2         ©¦ Condiments    ©¦ Sweet and savory sauces, relishes, spreads, and seasonings©¦
©¦ 3         ©¦ Confections   ©¦ Desserts, candies, and sweet breads                       ©¦
©¦ 4         ©¦ Dairy Products©¦ Cheeses                                                   ©¦
©¦ 5         ©¦ Grains/Cereals©¦ Breads, crackers, pasta, and cereal                       ©¦
©¦ 6         ©¦ Meat/Poultry  ©¦ Prepared meats                                            ©¦
©¦ 7         ©¦ Produce       ©¦ Dried fruit and bean curd                                 ©¦
©¦ 8         ©¦ Seafood       ©¦ Seaweed and fish                                          ©¦
©¸©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©Ø©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©Ø©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¼
", ConUtility.SeamlessTable(Categories));
        }

    }
}
