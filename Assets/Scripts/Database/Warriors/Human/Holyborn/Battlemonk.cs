public class Battlemonk {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 6, 6 },
            strength = new int[] { 2, 3 },
            health = new int[] { 7, 8 },
            speed = 2,
            range = 4,
            damageType = DamageType.Magical,
            race = Race.Holyborn,
            rarity = CardRarity.Rare,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.heal.Add(2, 3);
        ability.inspire.Add();

        return stats;
    }
}