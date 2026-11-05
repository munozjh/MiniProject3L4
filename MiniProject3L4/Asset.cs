using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace MiniProject3L4
{
    public enum AssetCategory
    {
        Computer,
        Mobile
    }

    internal abstract class Asset
    {
        public Asset(AssetCategory category, string brand, string model, DateTime purchaseDate, decimal priceUSD, string priceLocalCurrency, string office)
        {
            Category = category;
            Brand = brand;
            Model = model;
            PurchaseDate = purchaseDate;
            PriceUSD = priceUSD;
            PriceLocalCurrency = priceLocalCurrency;
            Office = office;
        }

        public AssetCategory Category { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PriceUSD { get; set; }
        public string PriceLocalCurrency { get; set; }
        public string Office { get; set; }
     
    }
}
