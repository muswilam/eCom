using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCom.Web.Code
{
    public enum SortByEnums : byte
    {
        Default = 1, 
        Popularity = 2,
        PriceLowToHigh = 3,
        PriceHighToLow = 4,
    }
}