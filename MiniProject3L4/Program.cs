using MiniProject3L4;
using System.Globalization;

AssetManager manager = new AssetManager();
CurrencyService currencyService = new CurrencyService();
Boolean exit = false;

// 1. EXPIRED (More than 3 years old - Past lifetime limit)
manager.AddAsset(new Computer("Dell", "OptiPlex 3040", new DateTime(2022, 5, 10), 600m, "6500 SEK", "Sweden", ComputerType.Desktop));
manager.AddAsset(new Mobile("Apple", "iPhone 8", new DateTime(2021, 2, 15), 300m, "1200 PLN", "Poland", MobileType.Smartphone));
manager.AddAsset(new Computer("HP", "ProBook 430", new DateTime(2023, 1, 20), 750m, "8000 SEK", "Sweden", ComputerType.Laptop));
manager.AddAsset(new Mobile("Samsung", "Galaxy S10", new DateTime(2020, 11, 01), 400m, "1500 PEN", "Peru", MobileType.Smartphone));
manager.AddAsset(new Computer("Lenovo", "ThinkPad T470", new DateTime(2019, 8, 12), 500m, "5500 SEK", "Sweden", ComputerType.Laptop));
// 2. LESS THAN 6 MONTHS REMAINING (Will be highlighted in RED)[cite: 2]
manager.AddAsset(new Computer("Acer", "Aspire 5", new DateTime(2024, 5, 15), 650m, "7000 SEK", "Sweden", ComputerType.Laptop));
manager.AddAsset(new Mobile("Motorola", "Moto G60", new DateTime(2024, 6, 01), 350m, "1400 PEN", "Peru", MobileType.Smartphone));
manager.AddAsset(new Computer("Apple", "iMac 27", new DateTime(2024, 4, 10), 1200m, "13000 SEK", "Sweden", ComputerType.Desktop));
manager.AddAsset(new Mobile("Xiaomi", "Redmi Note 10", new DateTime(2024, 6, 20), 250m, "1000 EUR", "Spain", MobileType.Smartphone));
manager.AddAsset(new Computer("Asus", "ZenBook 14", new DateTime(2024, 5, 05), 900m, "9500 SEK", "Sweden", ComputerType.Laptop));
// 3. LESS THAN 3 MONTHS REMAINING (Will be highlighted in YELLOW)[cite: 2]
manager.AddAsset(new Computer("Dell", "Latitude 5490", new DateTime(2023, 12, 10), 800m, "8500 SEK", "Sweden", ComputerType.Laptop));
manager.AddAsset(new Mobile("Google", "Pixel 6", new DateTime(2024, 1, 15), 500m, "2000 PLN", "Poland", MobileType.Smartphone));
manager.AddAsset(new Computer("HP", "EliteBook 840", new DateTime(2023, 11, 25), 950m, "10000 SEK", "Sweden", ComputerType.Laptop));
manager.AddAsset(new Mobile("OnePlus", "Nord 2", new DateTime(2024, 2, 02), 400m, "1600 PEN", "Peru", MobileType.Smartphone));
// 4. NORMAL / UP TO DATE (More than 6 months of useful life remaining)
manager.AddAsset(new Computer("Apple", "MacBook Pro M3", new DateTime(2026, 3, 15), 1800m, "19000 SEK", "Sweden", ComputerType.Laptop));
manager.AddAsset(new Mobile("Apple", "iPhone 16", new DateTime(2025, 10, 10), 1000m, "4000 PLN", "Poland", MobileType.Smartphone));
manager.AddAsset(new Computer("Lenovo", "ThinkCentre", new DateTime(2025, 7, 20), 700m, "7500 SEK", "Sweden", ComputerType.Desktop));
manager.AddAsset(new Mobile("Samsung", "Galaxy S25", new DateTime(2026, 2, 28), 900m, "3500 PEN", "Peru", MobileType.Smartphone));
manager.AddAsset(new Computer("Microsoft", "Surface Laptop", new DateTime(2025, 6, 11), 1100m, "12000 SEK", "Sweden", ComputerType.Laptop));
manager.AddAsset(new Mobile("Xiaomi", "Xiaomi 14", new DateTime(2025, 9, 05), 600m, "2400 EUR", "Spain", MobileType.Smartphone));

while (!exit)
{
    Console.WriteLine();
    Console.WriteLine("===================================================");
    Console.WriteLine("           COMPANY ASSET TRACKING SYSTEM           ");
    Console.WriteLine("===================================================");
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("1. Add Asset");
    Console.WriteLine("2. View Assets");
    Console.WriteLine("3. Search Assets");
    Console.WriteLine("4. Remove Asset");
    Console.WriteLine("5. Exit");
    Console.WriteLine();
    Console.WriteLine();
    Console.Write("Select Option: ");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            // Add Asset logic
            bool validInput = false;
            while (!validInput)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Select Asset Type:");
                Console.WriteLine("1. Computer");
                Console.WriteLine("2. Mobile");
                Console.WriteLine("3. Exit");
                Console.Write("Enter choice (1 or 2): ");
                string assetTypeChoice = Console.ReadLine();
                
                if (assetTypeChoice == "1")
                {
                    // Add Computer logic
                    
                    while (true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Select Computer Type:");
                        Console.WriteLine("1. Desktop");
                        Console.WriteLine("2. Laptop");
                        Console.Write("Enter choice (1 or 2): ");
                        string computerTypeChoice = Console.ReadLine();
                        if (computerTypeChoice == "1")
                        {
                            
                            Console.Write("Enter the brand:");
                            string brand = Console.ReadLine();
                            brand = manager.ValidateAsset(brand);

                            Console.Write("Enter the model:");
                            string model = Console.ReadLine();
                            model = manager.ValidateAsset(model);
                            DateTime purchaseDate;
                            while (true)
                            {
                                Console.Write("Enter the purchase date (MM/dd/yyyy):");
                                string inputDate = Console.ReadLine();

                                if (DateTime.TryParse(inputDate, out purchaseDate))
                                {
                                    break;
                                }

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid date format. Please use MM/dd/yyyy.");
                                Console.ResetColor();
                            }

                            Console.Write("Enter the price in USD:");
                            decimal priceUSD = decimal.Parse(Console.ReadLine());
                            Console.Write("Enter the office:");
                            string office = Console.ReadLine();
                            string priceLocalCurrencyFormatted = currencyService.ConvertToLocalCurrencyAsync(priceUSD, office).GetAwaiter().GetResult();
                            Computer computer = new Computer(brand, model, purchaseDate, priceUSD, priceLocalCurrencyFormatted, office, ComputerType.Desktop);
                            manager.AddAsset(computer);
                            
                            break;
                        }
                        else if (computerTypeChoice == "2")
                        {
                            Console.Write("Enter the brand: ");
                            string brand = Console.ReadLine();
                            brand = manager.ValidateAsset(brand);
                            Console.Write("Enter the model:");
                            string model = Console.ReadLine();
                            model = manager.ValidateAsset(model);
                            DateTime purchaseDate;
                            while (true)
                            {
                                Console.Write("Enter the purchase date (MM/dd/yyyy):");
                                string inputDate = Console.ReadLine();

                                if (DateTime.TryParse(inputDate, out purchaseDate))
                                {
                                    break; // validated date, proceed
                                }

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid date format. Please use MM/dd/yyyy.");
                                Console.ResetColor();
                            }

                            Console.Write("Enter the price in USD:");
                            decimal priceUSD = decimal.Parse(Console.ReadLine());
                            Console.Write("Enter the office:");
                            string office = Console.ReadLine();
                            string priceLocalCurrencyFormatted = currencyService.ConvertToLocalCurrencyAsync(priceUSD, office).GetAwaiter().GetResult();
                            Computer computer = new Computer(brand, model, purchaseDate, priceUSD, priceLocalCurrencyFormatted, office, ComputerType.Desktop);
                            manager.AddAsset(computer);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Please try again.");
                        }
                    }
                }   
                else if (assetTypeChoice == "2")
                {
                    while(true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Select Mobile Type:");
                        Console.WriteLine("1. Smartphone");
                        Console.WriteLine("2. Tablet");
                        Console.Write("Enter choice (1 or 2): ");
                        string mobileTypeChoice = Console.ReadLine();
                        if (mobileTypeChoice == "1")
                        {
                            Console.Write("Enter the brand:");
                            string brand = Console.ReadLine();
                            brand = manager.ValidateAsset(brand);
                            Console.Write("Enter the model:");
                            string model = Console.ReadLine();
                            model = manager.ValidateAsset(model);
                            DateTime purchaseDate;
                            while (true)
                            {
                                Console.Write("Enter the purchase date (MM/dd/yyyy):");
                                string inputDate = Console.ReadLine();
                                if (DateTime.TryParse(inputDate, out purchaseDate))
                                {
                                    break; // validated date, proceed
                                }
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid date format. Please use MM/dd/yyyy.");
                                Console.ResetColor();
                            }
                            Console.Write("Enter the price in USD:");
                            decimal priceUSD = decimal.Parse(Console.ReadLine());
                            Console.Write("Enter the office:");
                            string office = Console.ReadLine();
                            string priceLocalCurrencyFormatted = currencyService.ConvertToLocalCurrencyAsync(priceUSD, office).GetAwaiter().GetResult();
                            Mobile mobile = new Mobile(brand, model, purchaseDate, priceUSD, priceLocalCurrencyFormatted, office, MobileType.Smartphone);
                            manager.AddAsset(mobile);

                            break;
                        }
                        else if (mobileTypeChoice == "2")
                        {
                            Console.Write("Enter the brand: ");
                            string brand = Console.ReadLine();
                            brand = manager.ValidateAsset(brand);
                            Console.Write("Enter the model:");
                            string model = Console.ReadLine();
                            model = manager.ValidateAsset(model);
                            DateTime purchaseDate;
                            while (true)
                            {
                                Console.Write("Enter the purchase date (MM/dd/yyyy):");
                                string inputDate = Console.ReadLine();
                                if (DateTime.TryParse(inputDate, out purchaseDate))
                                {
                                    break; 
                                }
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid date format. Please use MM/dd/yyyy.");
                                Console.ResetColor();
                            }
                            Console.Write("Enter the price in USD:");
                            decimal priceUSD = decimal.Parse(Console.ReadLine());
                            Console.Write("Enter the office:");
                            string office = Console.ReadLine();
                            string priceLocalCurrencyFormatted = currencyService.ConvertToLocalCurrencyAsync(priceUSD, office).GetAwaiter().GetResult();
                            Mobile mobile = new Mobile(brand, model, purchaseDate, priceUSD, priceLocalCurrencyFormatted, office, MobileType.Tablet);
                            manager.AddAsset(mobile);

                            break;
                        }
                        else if (mobileTypeChoice == "2")
                        {
                            Console.Write("Enter the brand: ");
                            string brand = Console.ReadLine();
                            brand = manager.ValidateAsset(brand);
                            Console.Write("Enter the model:");
                            string model = Console.ReadLine();
                            model = manager.ValidateAsset(model);
                            DateTime purchaseDate;
                            while (true)
                            {
                                Console.Write("Enter the purchase date (MM/dd/yyyy):");
                                string inputDate = Console.ReadLine();
                                if (DateTime.TryParse(inputDate, out purchaseDate))
                                {
                                    break; 
                                }
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid date format. Please use MM/dd/yyyy.");
                                Console.ResetColor();
                            }
                            Console.Write("Enter the price in USD:");
                            decimal priceUSD = decimal.Parse(Console.ReadLine());
                            Console.Write("Enter the office:");
                            string office = Console.ReadLine();
                            string priceLocalCurrencyFormatted = currencyService.ConvertToLocalCurrencyAsync(priceUSD, office).GetAwaiter().GetResult();
                            Mobile mobile = new Mobile(brand, model, purchaseDate, priceUSD, priceLocalCurrencyFormatted, office, MobileType.Tablet);
                            manager.AddAsset(mobile);
                            break;
                        }
                    }
                
                }
                else if (assetTypeChoice == "3")
                {
                    validInput = true; // Exit the loop
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }

            }
            break;
        case "2":
            // View Assets logic
            manager.SortAssets();
            manager.ViewAssets();
            break;

        case "3":
            while (true)
            {
                Console.WriteLine();
                Console.Write("Search Assets by brand/model or type 'Exit' to return: ");
                string  searchTerm1 = Console.ReadLine();
                if (searchTerm1?.ToLower() == "exit")
                {
                    break;
                }
                string validatedTerm1 = manager.ValidateAsset(searchTerm1);
                manager.SearchAsset(validatedTerm1);
                
            }
            break;

        case "4":
            while (true)
            {
                Console.WriteLine();
                Console.Write("Enter the brand/model of the asset to remove or type 'Exit' to return: ");
                string searchTerm2 = Console.ReadLine();
                if (searchTerm2?.ToLower() == "exit")
                {
                    break;
                }
                string validatedTerm2 = manager.ValidateAsset(searchTerm2);
                manager.RemoveAsset(validatedTerm2);
            }
            

            break;
        case "5":
            exit = true;
            break;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}
