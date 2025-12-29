public class GraveDanger {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 5, 5 },
            strength = new int[] { 3, 5 },
            health = new int[] { 6, 6 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Gravedigger,
            rarity = CardRarity.Rare,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.soulCollect.Add(2, 3);
        ability.soulImbue.Add(1, 1);

        return stats;
    }
}