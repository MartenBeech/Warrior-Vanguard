public class MoonProwler {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 5, 5 },
            strength = new int[] { 3, 3 },
            health = new int[] { 7, 7 },
            speed = 3,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Werewolf,
            rarity = CardRarity.Rare,
            genre = Genre.Elves
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