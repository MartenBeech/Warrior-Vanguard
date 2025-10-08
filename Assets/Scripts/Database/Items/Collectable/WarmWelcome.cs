using System.Threading.Tasks;
using UnityEngine;

public class WarmWelcome : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Apply 1 Burning to summoned enemies";
        rarity = ItemRarity.Normal;
        genre = Genre.Underworld;
        return this;
    }

    public override async Task UseAfterEnemySummon(ItemTriggerParams parameters) {
        GridManager gridManager = FindFirstObjectByType<GridManager>();
        Warrior warrior = gridManager.GetCellWarrior(parameters.gridIndex);
        int burningAdded = 1;

        BunsenBurner bunsenBurner = new GameObject().AddComponent<BunsenBurner>();
        foreach (var item in ItemManager.items) {
            if (item.title == bunsenBurner.GetItem().title) {
                burningAdded++;
                break;
            }
        }

        warrior.stats.ability.burning.Add(burningAdded);
        await Task.Delay(0);
    }
}