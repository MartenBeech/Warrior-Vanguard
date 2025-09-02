public class SinisterHowler {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 5, 5 },
            strength = new int[] { 4, 5 },
            health = new int[] { 5, 5 },
            speed = 3,
            range = 2,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Ghoul,
            rarity = CardRarity.Rare,
            genre = Warrior.Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.cannibalism.Add(2, 3);
        ability.stealth.Add();

        return stats;
    }
}