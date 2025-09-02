public class HadesCompanion {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 1, 1 },
            strength = new int[] { 5, 7 },
            health = new int[] { 1, 1 },
            speed = 3,
            range = 2,
            damageType = Warrior.DamageType.Magical,
            race = Warrior.Race.Cerberus,
            rarity = CardRarity.Legendary,
            genre = Warrior.Genre.Underworld,
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