public class PlagueWalker {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 4, 4 },
            strength = new int[] { 2, 3 },
            health = new int[] { 5, 6 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Zombie,
            rarity = CardRarity.Common,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.revive.Add();
        ability.poisonCloud.Add(1, 2);

        return stats;
    }
}