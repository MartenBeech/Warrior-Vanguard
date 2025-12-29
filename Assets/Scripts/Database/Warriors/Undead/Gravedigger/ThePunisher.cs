public class ThePunisher {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 8, 8 },
            strength = new int[] { 1, 2 },
            health = new int[] { 3, 4 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Gravedigger,
            rarity = CardRarity.Legendary,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.soulCollect.Add(3, 4);
        ability.soulImbue.Add(1, 2);
        ability.purge.Add();

        return stats;
    }
}