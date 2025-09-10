public class CherryPicker {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 2,
            cost = new int[] { 3, 3 },
            strength = new int[] { 2, 2 },
            health = new int[] { 5, 7 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Farmer,
            rarity = CardRarity.Rare,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.farming.Add(1);

        return stats;
    }
}