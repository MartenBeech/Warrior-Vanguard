public class GrimReaper {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 7, 7 },
            strength = new int[] { 4, 6 },
            health = new int[] { 12, 12 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Reaper,
            rarity = CardRarity.Rare,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.darkTouch.Add(4, 6);
        ability.retaliate.Add();

        return stats;
    }
}