public class SilentAssasin {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 3, 3 },
            strength = new int[] { 3, 4 },
            health = new int[] { 3, 3 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Shade,
            rarity = CardRarity.Common,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.stealth.Add();
        ability.backstab.Add();

        return stats;
    }
}