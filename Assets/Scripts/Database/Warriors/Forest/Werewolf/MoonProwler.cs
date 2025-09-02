public class MoonProwler {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 5, 5 },
            strength = new int[] { 3, 3 },
            health = new int[] { 7, 7 },
            speed = 3,
            range = 2,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Werewolf,
            rarity = CardRarity.Rare,
            genre = Warrior.Genre.Forest
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.carnivore.Add();
        ability.vengeance.Add(1, 2);

        return stats;
    }
}