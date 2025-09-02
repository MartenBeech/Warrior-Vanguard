public class ArchPainmaker {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 10, 10 },
            strength = new int[] { 6, 8 },
            health = new int[] { 24, 32 },
            speed = 2,
            range = 2,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Demon,
            rarity = CardRarity.Legendary,
            genre = Warrior.Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.bloodPact.Add(3);
        ability.selfHarm.Add();
        ability.massSelfHarm.Add();

        return stats;
    }
}