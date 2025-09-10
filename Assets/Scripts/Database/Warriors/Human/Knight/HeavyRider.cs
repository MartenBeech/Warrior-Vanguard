public class HeavyRider {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 4, 4 },
            strength = new int[] { 0, 0 },
            health = new int[] { 6, 7 },
            speed = 4,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Knight,
            rarity = CardRarity.Common,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.joust.Add();
        ability.armor.Add(1, 2);

        return stats;
    }
}