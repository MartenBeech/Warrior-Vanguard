public class Whitefur {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 6, 6 },
            strength = new int[] { 1, 1 },
            health = new int[] { 10, 10 },
            speed = 3,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Werewolf,
            rarity = CardRarity.Legendary,
            genre = Genre.Elves,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.carnivore.Add();
        ability.vengeance.Add(1, 2);
        ability.bloodlust.Add(1, 2);

        return stats;
    }
}