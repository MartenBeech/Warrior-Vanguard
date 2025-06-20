public class WerewolfRunner {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            strength = new int[] { 2, 3 },
            health = new int[] { 5, 6 },
            speed = 3,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Werewolf,
            rarity = CardRarity.Common,
            genre = Character.Genre.Forest
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.carnivore.Add();

        return stats;
    }
}