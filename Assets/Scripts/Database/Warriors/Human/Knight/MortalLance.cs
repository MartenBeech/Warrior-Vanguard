public class MortalLance {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 4 },
            strength = new int[] { 2, 3 },
            health = new int[] { 5, 5 },
            speed = 4,
            range = 2,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Knight,
            rarity = CardRarity.Rare,
            genre = Warrior.Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.joust.Add();
        ability.bloodlust.Add(1, 2);

        return stats;
    }
}