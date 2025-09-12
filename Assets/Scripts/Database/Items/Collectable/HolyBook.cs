using System.Threading.Tasks;
using UnityEngine;

public class HolyBook : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "When you cast a spell on a friend, cast a copy on another friend";
        rarity = ItemRarity.Boss;
        return this;
    }
}