public class ElderwoodElder {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 9, 9 },
            strength = new int[] { 4, 5 },
            health = new int[] { 16, 19 },
            speed = 1,
            range = 1,
            damageType = DamageType.Physical,
            race = Race.Treant,
            rarity = CardRarity.Common,
            genre = Genre.Forest
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.rooting.Add();
        ability.sprout.Add();

        return stats;
    }
}