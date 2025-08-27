using UnityEngine;

public class RingOfRace : Item {
    string raceKey = "RingsBlessingRaceKey";
    public override Item GetItem() {
        title = GetType().Name;
        string race = PlayerPrefs.GetString(raceKey, "NoRace");
        description = $"Every friendly {race} gain 2 health";
        return this;
    }

    public override void UseOnFriendSpawn(ItemTriggerParams parameters) {
        string race = PlayerPrefs.GetString(raceKey, "NoRace");
        if (parameters.stats.race == (Character.Race)System.Enum.Parse(typeof(Character.Race), race)) {
            parameters.stats.AddHealth(2);
        }
    }
}