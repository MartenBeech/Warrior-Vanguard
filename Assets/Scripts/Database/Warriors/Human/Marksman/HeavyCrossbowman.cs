public class HeavyCrossbowman {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 6, 6 },
            strength = new int[] { 7, 9 },
            health = new int[] { 12, 14 },
            speed = 2,
            range = 4,
            damageType = DamageType.Physical,
            race = Race.Marksman,
            rarity = CardRarity.Common,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.reload.Add();
        ability.knockBack.Add();

        return stats;
    }
}