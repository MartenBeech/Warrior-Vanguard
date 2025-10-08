using System.Threading.Tasks;
using UnityEngine;

public class BunsenBurner : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "When you apply Burning, apply 1 more stack";
        rarity = ItemRarity.Normal;
        genre = Genre.Underworld;
        return this;
    }
}