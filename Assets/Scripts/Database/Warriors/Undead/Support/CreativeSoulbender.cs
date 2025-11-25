public class CreativeSoulbender {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 2, 2 },
            strength = new int[] { 1, 2 },
            health = new int[] { 1, 2 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Support,
            rarity = CardRarity.Rare,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.afterlife.Add();
        ability.drawing.Add(1);

        return stats;
    }
}