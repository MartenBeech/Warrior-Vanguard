using UnityEngine;

public class RingOfRace : Item {
    string raceKey = "RingsBlessingRaceKey";
    public override Item GetItem() {
        title = GetType().Name;
        string race = PlayerPrefs.GetString(raceKey, "NoRace");
        description = $"Every friendly {race} gain 2 health";
        rarity = ItemManager.Rarity.Special;
        return this;
    }

    public override void UseOnFriendSummon(ItemTriggerParams parameters) {
        string race = PlayerPrefs.GetString(raceKey, "NoRace");
        if (parameters.stats.race == (Warrior.Race)System.Enum.Parse(typeof(Warrior.Race), race)) {
            parameters.stats.AddHealth(2);
        }
    }
}