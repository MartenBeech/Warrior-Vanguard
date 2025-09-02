public class LightkeeperZealot {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 7, 7 },
            strength = new int[] { 4, 4 },
            health = new int[] { 7, 9 },
            speed = 2,
            range = 4,
            damageType = Warrior.DamageType.Magical,
            race = Warrior.Race.Holyborn,
            rarity = CardRarity.Legendary,
            genre = Warrior.Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.heal.Add(5, 5);
        ability.massHeal.Add(1, 2);

        return stats;
    }
}