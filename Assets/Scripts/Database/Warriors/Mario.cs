public class Mario : WarriorStats {
    public static WarriorStats GetCard() {
        WarriorStats stats = new() {
            title = "Mario",
            attack = 2,
            health = 10,
            cost = 1,
            movementSpeed = 2,
            range = 2,
            numberOfAttacks = 1,
        };
        stats.healthMax = stats.health;

        stats.ability.bloodlust.Add(1);
        stats.ability.revive.Add();

        return stats;
    }
}