public class SoulStealer {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 7, 7 },
            strength = new int[] { 0, 0 },
            health = new int[] { 5, 7 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Wraith,
            rarity = CardRarity.Legendary,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }
        stats.rarity = CardRarity.Legendary;

        WarriorAbility ability = stats.ability;
        ability.afterlife.Add();
        ability.darkTouch.Add(5, 7);
        ability.possess.Add();

        return stats;
    }
}