public class FlameWarden {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 5, 5 },
            strength = new int[] { 5, 6 },
            health = new int[] { 2, 3 },
            speed = 2,
            range = 4,
            damageType = Warrior.DamageType.Magical,
            race = Warrior.Race.Succubus,
            rarity = CardRarity.Common,
            genre = Warrior.Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.enflame.Add();
        ability.immolate.Add(2, 3);

        return stats;
    }
}