public class MinotaurKing {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 7, 7 },
            strength = new int[] { 6, 6 },
            health = new int[] { 6, 6 },
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Minotaur,
            rarity = CardRarity.Legendary,
            genre = Character.Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.evilInspiration.Add(1, 2);

        return stats;
    }
}