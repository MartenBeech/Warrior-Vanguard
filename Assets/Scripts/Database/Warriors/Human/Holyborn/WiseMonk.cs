public class WiseMonk {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 5, 5 },
            strength = new int[] { 3, 4 },
            health = new int[] { 5, 6 },
            speed = 2,
            range = 4,
            damageType = Character.DamageType.Magical,
            race = Character.Race.Holyborn,
            rarity = CardRarity.Rare,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.heal.Add(3, 4);

        return stats;
    }
}