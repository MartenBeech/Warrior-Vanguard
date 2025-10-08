using System.Threading.Tasks;
using UnityEngine;

public class HeroParrot : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Your hero power trigger an additional time";
        rarity = ItemRarity.Boss;
        genre = Genre.None;
        return this;
    }
}