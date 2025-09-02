public class CreativeSoulbender {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            strength = new int[] { 1, 2 },
            health = new int[] { 1, 2 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Wraith,
            rarity = CardRarity.Common,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }
        stats.rarity = CardRarity.Legendary;

        WarriorAbility ability = stats.ability;
        ability.afterlife.Add();
        ability.drawing.Add(1);

        return stats;
    }
}