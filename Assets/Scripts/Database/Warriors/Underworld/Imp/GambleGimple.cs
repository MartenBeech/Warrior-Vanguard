public class GambleGimple {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 4 },
            strength = new int[] { 4, 5 },
            health = new int[] { 3, 3 },
            speed = 2,
            range = 4,
            damageType = Warrior.DamageType.Magical,
            race = Warrior.Race.Imp,
            rarity = CardRarity.Legendary,
            genre = Warrior.Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.raceDiscount.Add(3);

        return stats;
    }
}