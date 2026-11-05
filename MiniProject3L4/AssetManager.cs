using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
namespace MiniProject3L4
{
    internal class AssetManager
    {
      public List<Asset> Assets { get; private set; }
        public AssetManager()
        {
            Assets = new List<Asset>();
        }
        // Validates the asset input to ensure it is not null, empty, or whitespace
        public string ValidateAsset (string asset)
        {
            string Asset;
            while (true)
            {
                if (string.IsNullOrEmpty(asset) || string.IsNullOrWhiteSpace(asset) )
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Category cannot be empty. Please enter a valid category.");
                    Console.ResetColor();
                    continue;

                }
                Asset = asset;
                break;
            }
            return Asset;
        }
        // Adds a new asset to the list of assets
        public void AddAsset(Asset asset)
        {
            Assets.Add(asset);
        }
        // Sorts the assets first by office, then by specific type, and finally by purchase date
        public void SortAssets()
        {
            Assets = Assets.OrderBy(a=>a.Office).ThenBy(a => a switch
            {
                Computer c => c.ComputerType.ToString(),
                Mobile m => m.MobileType.ToString(),
                _ => ""
            }).ThenBy(a => a.PurchaseDate).ToList();
        }
        public void ViewAssets()
        {

            if (Assets.Count == 0)
            {
                Console.WriteLine("No assets available.");
                return;
            }
            Console.WriteLine();
            Console.WriteLine("===============================================================================================================");
            Console.WriteLine($"                                                       ASSET LIST                                                                       ");
            Console.WriteLine("===============================================================================================================");
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"Office",-15} {"Type",-15} {"Brand",-15} {"Model",-15} {"Purchase Date",-15} {"Price (USD)",-15} {"Price (Local)",-15}");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------"); ;
            Console.WriteLine();
            foreach (var asset in Assets)
            {
                string specificType = asset switch
                {
                    Computer c => c.ComputerType.ToString(),
                    Mobile m => m.MobileType.ToString(),
                    _ => "Unknown"
                };
                string formattedPriceUSD = asset.PriceUSD.ToString("C0", new CultureInfo("en-US"));
                Console.ForegroundColor = GetAssetColor(asset);
                Console.WriteLine($"{asset.Office,-15} {specificType,-15} {asset.Brand,-15} {asset.Model,-15} {asset.PurchaseDate.ToShortDateString(),-15} {formattedPriceUSD,-15} {asset.PriceLocalCurrency,-15}");
                Console.ResetColor();
            }

        }
        public void SearchAsset(String searchTerm)
        { 
            List<Asset> searchResults = Assets.Where(a => a.Brand.ToLower().Contains(searchTerm.ToLower()) || a.Model.ToLower().Contains(searchTerm.ToLower())).ToList();
            if (searchResults.Count == 0)
            {
                Console.WriteLine($"No assets found matching the search term: {searchTerm}");
                return;
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("FOUND ASSESTS:                                                       ");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"Office",-15} {"Type",-15} {"Brand",-15} {"Model",-15} {"Purchase Date",-15} {"Price (USD)",-15} {"Price (Local)",-15}");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            Console.ResetColor();
            foreach (var asset in searchResults)
            {
                string specificType = asset switch
                {
                    Computer c => c.ComputerType.ToString(),
                    Mobile m => m.MobileType.ToString(),
                    _ => "Unknown"
                };
                string formattedPriceUSD = asset.PriceUSD.ToString("C0", new CultureInfo("en-US"));
                Console.ForegroundColor = GetAssetColor(asset);
                Console.WriteLine($"{asset.Office,-15} {specificType,-15} {asset.Brand,-15} {asset.Model,-15} {asset.PurchaseDate.ToShortDateString(),-15} {formattedPriceUSD,-15} {asset.PriceLocalCurrency,-15}");
                Console.ResetColor();
            }
        }
        public void RemoveAsset(String searchTerm)
        {
            List<Asset> searchResults = Assets.Where(a => a.Brand.ToLower().Contains(searchTerm.ToLower()) || a.Model.ToLower().Contains(searchTerm.ToLower())).ToList();

            if (searchResults.Count == 0)
            {
                Console.WriteLine($"No assets found matching the search term: {searchTerm}");
                return;
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"FOUND {searchResults.Count} MATCHING ASSET(S):");
            Console.WriteLine();
            Console.ResetColor();

            // 2. Iterate through the search results and ask the user if they want to delete each asset
            foreach (var asset in searchResults)
            {
                string specificType = asset switch
                {
                    Computer c => c.ComputerType.ToString(),
                    Mobile m => m.MobileType.ToString(),
                    _ => "Unknown"
                };
                string formattedPriceUSD = asset.PriceUSD.ToString("C0", new CultureInfo("en-US"));
                Console.ForegroundColor = GetAssetColor(asset);
                Console.WriteLine($"{asset.Office,-15} {specificType,-15} {asset.Brand,-15} {asset.Model,-15} {asset.PurchaseDate.ToShortDateString(),-15} {formattedPriceUSD,-15} {asset.PriceLocalCurrency,-15}");
                Console.ResetColor();

                // Ask the user if they want to delete this asset
                while (true)
                {
                    Console.WriteLine();
                    Console.Write("Do you want to delete this asset? (y/n): ");
                    string deleteChoice = Console.ReadLine()?.Trim().ToLower();

                    if (deleteChoice == "y")
                    {
                        // Removed the asset from the list
                        Assets.Remove(asset);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine();
                        Console.WriteLine("-> Asset deleted successfully.");
                        Console.WriteLine();
                        Console.ResetColor();
                        break;
                    }
                    else if (deleteChoice == "n")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine();
                        Console.WriteLine("-> Asset kept.");
                        Console.WriteLine();
                        Console.ResetColor();
                        break; 
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                        Console.ResetColor();
                    }
                }
            }
        }
        public ConsoleColor GetAssetColor(Asset asset)
        {
            DateTime expirationDate = asset.PurchaseDate.AddYears(3);

            if (DateTime.Now > expirationDate)
            {
                return ConsoleColor.DarkBlue; // Expaired
            }
            else if (DateTime.Now.AddMonths(3) > expirationDate)
            {
                return ConsoleColor.Yellow; // less than 3 months
            }
            else if (DateTime.Now.AddMonths(6) > expirationDate)
            {
                return ConsoleColor.Red;    // less than 6 months
            }
            else
            {
                return ConsoleColor.Gray;   // defalt color for assets with more than 6 months remaining
            }
        }
        public List<Asset> GetAssets()
        {
            return Assets;
        }

        internal string ValidateAsset(object searchTerm2)
        {
            throw new NotImplementedException();
        }
    }
}
