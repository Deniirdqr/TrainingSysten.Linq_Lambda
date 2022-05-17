using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using TrainingTwenty.Entities;

namespace Course
{
    class Program
    {
        static void Print<T>(string message, IEnumerable<T> collection)
        {
            Console.WriteLine(message);
            foreach(T obj in collection)
            {
                Console.WriteLine(obj);
            }
        }
        static void Main(string[] args)
        {
            Category c1 = new Category() { Id = 1, Name = "Tools", Tier = 2 };
            Category c2 = new Category() { Id = 2, Name = "Computers", Tier = 1 };
            Category c3 = new Category() { Id = 3, Name = "Eletronics", Tier = 3 };

            List<Product> product = new List<Product>()
            {
                new Product(){ Id = 1, Name = "Computer", Price = 1100.0, Category = c2},
                new Product(){ Id = 2, Name = "Hammer", Price = 90.0, Category = c1},
                new Product(){ Id = 3, Name = "TV", Price = 1700.0, Category = c3},
                new Product(){ Id = 4, Name = "NotBook", Price = 1300.0, Category = c2},
                new Product(){ Id = 5, Name = "Saw", Price = 80.0, Category = c1},
                new Product(){ Id = 6, Name = "Tablet", Price = 700.0, Category = c2},
                new Product(){ Id = 7, Name = "Camera", Price = 700.0, Category = c3},
                new Product(){ Id = 8, Name = "Printer", Price = 350.0, Category = c3},
                new Product(){ Id = 9, Name = "MacBook", Price = 1800.0, Category = c2},
                new Product(){ Id = 10, Name = "Sound Bar", Price = 700.0, Category = c3},
                new Product(){ Id = 11, Name = "Lever", Price = 70.0, Category = c1}
            };
            var r1 = product.Where( p => p.Category.Tier == 1 && p.Price < 900.0);
            Print("TIER 1 AND PRICE < 900: ", r1);
            Console.WriteLine();

            var r2 = product.Where( p => p.Category.Name == "Tools").Select(p => p.Name);
            Print("NAMES OF PRODUCTS FROM TOOLS", r2);
            Console.WriteLine();

            var r3 = product.Where( p => p.Name[0] == 'C').Select(p => new {p.Price, p.Name, ProCategory = p.Category.Name});
            Print("NAMES STARTED WITH 'C' AND ANONYMOUS OBJECT", r3);
            Console.WriteLine();

            var r4 = product.Where(p => p.Category.Tier == 1).OrderBy(p => p.Price).ThenBy(p => p.Name);
            Print("TIER 1 ORDER BY PRICE AND NAME", r4);
            Console.WriteLine();

            var r5 = r4.Skip(2).Take(3);
            Print("TIER 1 ORDER BY PRICE JUMP 2 AND TAKE 3", r5);
            Console.WriteLine();

            var r6 = product.First();
            Console.WriteLine("Firts Test 1 " + r6);
            Console.WriteLine();

            var r7 = product.Where(p => p.Price > 3000.0).FirstOrDefault();
            Console.WriteLine("Firts or Default Test 1 " + r7);
            Console.WriteLine();

            var r8 = product.Where(p => p.Id == 3).SingleOrDefault();
            Console.WriteLine("Single or Default Test 1 " + r8);
            Console.WriteLine();

            var r9 = product.Max(p => p.Price);
            Console.WriteLine("Max Price of Products: " + r9);
            var r10 = product.Min(p => p.Price);
            Console.WriteLine("Min Price of Products: " + r10);
            var r11 = product.Where(p => p.Category.Id == 1).Sum(p => p.Price);
            Console.WriteLine("Category 1 sum Prices: " + r11);
            var r12 = product.Where(p => p.Category.Id == 1).Average(p => p.Price);
            Console.WriteLine("Category 1 Average Prices: " + r12);
            var r13 = product.Where(p => p.Category.Id == 5).Select(p => p.Price).DefaultIfEmpty(0.0).Average();
            Console.WriteLine("Error Tratament in null Category: " + r13);
            var r14 = product.Where(p => p.Category.Id == 1).Select(p => p.Price).Aggregate(0.0, (x, y) => x + y);
            Console.WriteLine("Category 1 Aggregate Prices: " + r14);
            Console.WriteLine();

            var r15 = product.GroupBy(p => p.Category);
            foreach(IGrouping<Category, Product> group in r15)
            {
                Console.WriteLine("Category " + group.Key.Name + ":");
                {
                    foreach(Product p in group)
                    {
                        Console.WriteLine(p);
                    }
                    Console.WriteLine();
                }
            }

        }
    }
}