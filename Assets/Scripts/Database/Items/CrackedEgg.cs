using System.Text.RegularExpressions;
using UnityEngine;

public class CrackedEgg : Item {
    public Item GetItem() {
        title = GetType().Name;
        description = "When you summon a 6+ cost warrior, it summons a 1 cost warrior";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }

    public override async void UseOnWarriorSpawn(WarriorStats stats, Vector2 gridIndex) {
        if (stats.cost >= 6) {
            CharacterSpawner characterSpawner = FindFirstObjectByType<CharacterSpawner>();
            WarriorStats random1Cost = CardDatabase.GetRandomWarriorWithSpecificCost(1);
            random1Cost.alignment = stats.alignment;

            await characterSpawner.SpawnRandomly(random1Cost, gridIndex);
        }
    }

    public override void UseImmediately() {

    }
}