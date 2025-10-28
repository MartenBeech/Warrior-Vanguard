public class Centaura {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 6, 6 },
            strength = new int[] { 3, 3 },
            health = new int[] { 7, 7 },
            speed = 4,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Centaur,
            rarity = CardRarity.Legendary,
            genre = Genre.Elves,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.hitAndRun.Add();
        ability.forestStrength.Add(1, 2);

        return stats;
    }
}