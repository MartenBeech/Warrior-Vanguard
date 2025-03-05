using System.Collections.Generic;

public class WarriorStats {
    public string title = "";
    public int attack = 0;
    public int health = 1;
    public int cost = 0;
    public int movementSpeed = 1;
    public int range = 1;
    public int numberOfAttacks = 1;
    public Dictionary<WarriorAbility.Ability, int> abilities = new();

    public void SetStats(WarriorStats stats) {
        title = stats.title;
        attack = stats.attack;
        health = stats.health;
        cost = stats.cost;
        movementSpeed = stats.movementSpeed;
        range = stats.range;
        numberOfAttacks = stats.numberOfAttacks;
        abilities = stats.abilities;
    }
}