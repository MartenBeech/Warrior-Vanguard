public class FrenziedGhoul {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 3 },
            strength = new int[] { 3, 3 },
            health = new int[] { 4, 5 },
            speed = 3,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Ghoul,
            rarity = CardRarity.Common,
            genre = Character.Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.cannibalism.Add(2, 3);

        return stats;
    }
}