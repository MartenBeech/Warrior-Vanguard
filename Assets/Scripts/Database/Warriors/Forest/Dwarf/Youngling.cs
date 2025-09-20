public class Youngling {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 1, 1 },
            strength = new int[] { 1, 1 },
            health = new int[] { 3, 4 },
            speed = 1,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Dwarf,
            rarity = CardRarity.Common,
            genre = Genre.Elves,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.bash.Add();
        ability.resistance.Add(1);

        return stats;
    }
}