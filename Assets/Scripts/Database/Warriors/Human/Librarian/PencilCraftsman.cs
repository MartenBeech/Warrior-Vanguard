public class PencilCraftsman {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 5, 5 },
            strength = new int[] { 4, 5 },
            health = new int[] { 4, 5 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Librarian,
            rarity = CardRarity.Rare,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.drawing.Add(2);

        return stats;
    }
}