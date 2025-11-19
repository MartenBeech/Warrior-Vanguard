public class Boney {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 1, 1 },
            strength = new int[] { 1, 1 },
            health = new int[] { 2, 3 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Reaper,
            rarity = CardRarity.Common,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.darkTouch.Add(2, 3);

        return stats;
    }
}