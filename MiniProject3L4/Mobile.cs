using System;
using System.Collections.Generic;
using System.Text;

namespace MiniProject3L4
{
    public enum MobileType
    {
        Smartphone,
        Tablet
    }
    internal class Mobile:Asset
    {
        public Mobile(string brand, string model, DateTime purchaseDate, decimal priceUSD, string priceLocalCurrency, string office, MobileType mobileType)
            : base(AssetCategory.Mobile, brand, model, purchaseDate, priceUSD, priceLocalCurrency, office)
        {
            MobileType = mobileType;
        }
        public MobileType MobileType { get; set; }

    }
}
