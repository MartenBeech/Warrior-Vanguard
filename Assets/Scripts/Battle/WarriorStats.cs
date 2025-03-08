using System.Collections.Generic;

public class WarriorStats {
    public string title = "";
    public int attack = 0;
    public int health = 0;
    public int healthMax = 0;
    public int cost = 0;
    public int movementSpeed = 2;
    public int range = 2;
    public int numberOfAttacks = 1;
    public WarriorAbility ability = new();

    public void SetStats(WarriorStats stats) {
        title = stats.title;
        attack = stats.attack;
        health = stats.health;
        healthMax = stats.healthMax;
        cost = stats.cost;
        movementSpeed = stats.movementSpeed;
        range = stats.range;
        numberOfAttacks = stats.numberOfAttacks;
        ability = stats.ability;
    }
}