public class YoungPriestess {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            strength = new int[] { 1, 1 },
            health = new int[] { 3, 4 },
            speed = 2,
            range = 4,
            damageType = DamageType.Magical,
            race = Race.Holyborn,
            rarity = CardRarity.Common,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.heal.Add(1, 2);

        return stats;
    }
}