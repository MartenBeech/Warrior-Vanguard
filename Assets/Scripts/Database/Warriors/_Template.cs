public class CLASSNAMEWARRIOR {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 0, 0 },
            strength = new int[] { 0, 0 },
            health = new int[] { 0, 0 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.None,
            rarity = CardRarity.None,
            genre = Genre.None,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;

        return stats;
    }
}