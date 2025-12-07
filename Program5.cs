using System;
using System.Collections.Generic;

namespace SydneyCoffee
{
    class Customer
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool IsReseller { get; set; }
        public double Charge { get; set; }
    }

    class Program5
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t\tWelcome to use Sydney Coffee Program\n");

            int n = ReadIntInRange("Enter number of customers: ", 1, 200);
            List<Customer> customers = new List<Customer>();

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\n--- Customer {i + 1} ---");

                Customer c = new Customer();

                Console.Write("Enter customer name: ");
                c.Name = Console.ReadLine();

                c.Quantity = ReadIntInRange("Enter the number of coffee beans bags (bag/1kg): ", 1, 200);

                c.IsReseller = ReadYesNo("Enter yes/no to indicate whether you are a reseller: ");

                c.Charge = CalculateCharge(c.Quantity, c.IsReseller);

                Console.WriteLine($"The total sales value from {c.Name} is ${c.Charge:F2}");
                Console.WriteLine("-----------------------------------------------------------------------------");

                customers.Add(c);
            }

            PrintSummary(customers);
        }

        // HELPER METHODS
        static int ReadIntInRange(string prompt, int min, int max)
        {
            int value;
            bool valid;

            do
            {
                Console.Write(prompt);
                valid = int.TryParse(Console.ReadLine(), out value) &&
                        value >= min && value <= max;

                if (!valid)
                {
                    Console.WriteLine($"Invalid Input!\nValue must be between {min} and {max}.\n");
                }
            } while (!valid);

            return value;
        }

        static bool ReadYesNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().Trim().ToLower();
                if (input == "yes") return true;
                if (input == "no") return false;

                Console.WriteLine("Please enter 'yes' or 'no'.\n");
            }
        }

        static double CalculateCharge(int quantity, bool isReseller)
        {
            double unitPrice;

            if (quantity <= 5)
                unitPrice = 36;
            else if (quantity <= 15)
                unitPrice = 34.5;
            else
                unitPrice = 32.7;

            double price = unitPrice * quantity;

            if (isReseller)
                price *= 0.8; // 20% discount

            return price;
        }

        static void PrintSummary(List<Customer> customers)
        {
            Console.WriteLine("\n\t\t\t\t\tSummary of sales\n");
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine("{0,15}{1,10}{2,10}{3,10}", "Name", "Quantity", "Reseller", "Charge");

            double min = double.MaxValue;
            double max = double.MinValue;
            string minName = "";
            string maxName = "";

            foreach (var c in customers)
            {
                Console.WriteLine("{0,15}{1,10}{2,10}{3,10:F2}",
                    c.Name, c.Quantity, c.IsReseller ? "yes" : "no", c.Charge);

                if (c.Charge < min)
                {
                    min = c.Charge;
                    minName = c.Name;
                }

                if (c.Charge > max)
                {
                    max = c.Charge;
                    maxName = c.Name;
                }
            }

            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine($"The customer spending most is {maxName} ${max:F2}");
            Console.WriteLine($"The customer spending least is {minName} ${min:F2}");
        }
    }
}
