public class Centarcher {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 3 },
            strength = new int[] { 4, 5 },
            health = new int[] { 1, 2 },
            speed = 4,
            range = 4,
            damageType = DamageType.Physical,
            race = Race.Centaur,
            rarity = CardRarity.Common,
            genre = Genre.Elves,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.hitAndRun.Add();

        return stats;
    }
}