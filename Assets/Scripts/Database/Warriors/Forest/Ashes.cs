public class Ashes {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 0, 0 },
            strength = new int[] { 0, 0 },
            health = new int[] { 1, 0 },
            speed = 0,
            range = 0,
            damageType = DamageType.Physical,
            race = Race.Dragon,
            rarity = CardRarity.None,
            genre = Genre.Forest,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.rebirth.Add();

        return stats;
    }
}