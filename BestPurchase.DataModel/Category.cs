using System;
using System.ComponentModel.DataAnnotations;

namespace BestPurchase.DataModel
{
    [Serializable]
    public enum Category
    {
        OrganicProducts = 1,
        MeatProducts,
        FishAndSeaFood,
        FrozenProducts,
        Sweets
    }
}