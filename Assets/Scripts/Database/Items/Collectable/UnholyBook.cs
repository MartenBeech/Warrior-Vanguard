using System.Threading.Tasks;
using UnityEngine;

public class UnholyBook : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "When you cast a spell on an enemy, cast a copy on another enemy";
        rarity = ItemRarity.Boss;
        genre = Genre.None;
        return this;
    }
}