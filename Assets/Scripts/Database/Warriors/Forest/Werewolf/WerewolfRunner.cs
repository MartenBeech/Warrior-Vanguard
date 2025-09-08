public class WerewolfRunner {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            strength = new int[] { 2, 3 },
            health = new int[] { 5, 6 },
            speed = 3,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Werewolf,
            rarity = CardRarity.Common,
            genre = Genre.Elves
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.carnivore.Add();

        return stats;
    }
}