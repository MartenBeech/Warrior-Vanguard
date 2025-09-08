public class SpiritualGrounds {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 3 },
            strength = new int[] { 0, 0 },
            health = new int[] { 4, 4 },
            speed = 0,
            range = 0,
            damageType = DamageType.Physical,
            race = Race.Construct,
            rarity = CardRarity.Rare,
            genre = Genre.Elves,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.construct.Add();
        ability.summoningSpirits.Add();

        return stats;
    }
}