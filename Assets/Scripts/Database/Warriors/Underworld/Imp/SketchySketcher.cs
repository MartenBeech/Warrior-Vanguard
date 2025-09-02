public class SketchySketcher {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            strength = new int[] { 1, 2 },
            health = new int[] { 2, 2 },
            speed = 2,
            range = 4,
            damageType = DamageType.Magical,
            race = Race.Imp,
            rarity = CardRarity.Common,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.drawing.Add(1);

        return stats;
    }
}