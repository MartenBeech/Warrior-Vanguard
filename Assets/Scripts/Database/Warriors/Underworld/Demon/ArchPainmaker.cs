public class ArchPainmaker {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 10, 10 },
            strength = new int[] { 6, 8 },
            health = new int[] { 24, 32 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Demon,
            rarity = CardRarity.Legendary,
            genre = Genre.Underworld,
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