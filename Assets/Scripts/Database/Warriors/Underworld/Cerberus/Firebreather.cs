public class Firebreather {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 3, 3 },
            strength = new int[] { 2, 3 },
            health = new int[] { 4, 5 },
            speed = 3,
            range = 2,
            damageType = DamageType.Magical,
            race = Race.Cerberus,
            rarity = CardRarity.Common,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.cleave.Add();
        ability.enflame.Add();

        return stats;
    }
}