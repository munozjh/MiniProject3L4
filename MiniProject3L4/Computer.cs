using System;
using System.Collections.Generic;
using System.Text;

namespace MiniProject3L4
{
    public enum ComputerType
    {
        Desktop,
        Laptop
    }
    internal class Computer:Asset
    {   
        public Computer(string brand, string model, DateTime purchaseDate, decimal priceUSD, string priceLocalCurrency, string office, ComputerType computerType)
            : base(AssetCategory.Computer, brand, model, purchaseDate, priceUSD, priceLocalCurrency, office)
        {
            ComputerType = computerType;
        }
        public ComputerType ComputerType { get; set; }

    }
}
