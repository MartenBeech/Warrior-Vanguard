public class NightChaser {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 5, 5 },
            strength = new int[] { 4, 4 },
            health = new int[] { 6, 7 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Shade,
            rarity = CardRarity.Rare,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.stealth.Add();
        ability.backstab.Add();
        ability.cloak.Add();

        return stats;
    }
}