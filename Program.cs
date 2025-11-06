using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Step 1: Get customer info
        Console.WriteLine("Welcome to Intergalactic Pizza Co!");
        Console.Write("Customer name: ");
        string name = Console.ReadLine();

        Console.WriteLine("Destination planet (Earth, Mars, Jupiter Station, Venus Outpost):");
        Console.Write("Planet: ");
        string planet = Console.ReadLine();

        // Step 2: Show menu and take orders
        var menu = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase)
        {
            { "Galactic Cheese", 10m },
            { "Meteor Meat Lover", 15m },
            { "Veggie Nebula", 12m },
            { "Black Hole BBQ", 18m }
        };

        Console.WriteLine("\n--- Menu ---");
        foreach (var item in menu)
        {
            Console.WriteLine($"{item.Key} - {item.Value} credits");
        }

        Console.WriteLine("Type pizza name to add, or 'done' to finish.");

        var order = new Dictionary<string, int>();

        while (true)
        {
            Console.WriteLine("Type a pizza name, or 'done' to finish:");
            string choice = Console.ReadLine();

            if (choice.ToLower() == "done")
                break;

            if (!menu.ContainsKey(choice))
            {
                Console.WriteLine("That pizza isn’t on the menu. Try again HUMON!.");
                continue;
            }

            Console.Write("Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
            {
                Console.WriteLine("Invalid quantity. Try again.");
                continue;
            }

            if (order.ContainsKey(choice))
                order[choice] += qty;
            else
                order.Add(choice, qty);
        }

        if (order.Count == 0)
        {
            Console.WriteLine("\nNo items ordered. Goodbye.");
            return;
        }

        // Step 3: Delivery fees by planet
        decimal delivery = planet switch
        {
            "Earth" => 5m,
            "Mars" => 10m,
            "Jupiter Station" => 15m,
            "Venus Outpost" => 8m,
            _ => 0m // if user typed something invalid, just 0 fee
        };

        // Step 4: Discounts and totals
        decimal subtotal = 0m;
        int totalPizzas = 0;

        Console.WriteLine($"\nOrder for {name} to {planet}");

        foreach (var item in order)
        {
            string pizza = item.Key;
            int qty = item.Value;
            decimal price = menu[pizza];
            decimal lineTotal = qty * price;

            Console.WriteLine($"{qty} x {pizza} @ {price:0.##} = {lineTotal:0.##}");

            subtotal += lineTotal;
            totalPizzas += qty;
        }

        decimal discount = 0m;
        if (totalPizzas >= 3)
        {
            discount = Math.Round(subtotal * 0.10m, 2);
        }

        decimal total = subtotal - discount + delivery;

        // Step 5: Show summary
        Console.WriteLine($"Subtotal: {subtotal:0.##}");
        if (discount > 0)
            Console.WriteLine($"Discount (10%): -{discount:0.##}");
        Console.WriteLine($"Delivery Fee: {delivery:0.##}");
        Console.WriteLine($"Total Due: {total:0.##} credits");
    }
}
