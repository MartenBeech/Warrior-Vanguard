public class FlameWarden {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 5, 5 },
            strength = new int[] { 3, 4 },
            health = new int[] { 2, 2 },
            speed = 2,
            range = 4,
            damageType = Character.DamageType.Magical,
            race = Character.Race.Succubus,
            rarity = CardRarity.Common,
            genre = Character.Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.enflame.Add(3, 4);
        ability.immolate.Add(2, 3);

        return stats;
    }
}