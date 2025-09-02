public class Apprentice {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 1, 1 },
            strength = new int[] { 1, 2 },
            health = new int[] { 1, 1 },
            speed = 2,
            range = 4,
            damageType = Warrior.DamageType.Magical,
            race = Warrior.Race.Sorcerer,
            rarity = CardRarity.Common,
            genre = Warrior.Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.lightningBolt.Add(1, 2);

        return stats;
    }
}