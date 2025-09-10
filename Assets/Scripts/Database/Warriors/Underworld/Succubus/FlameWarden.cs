public class FlameWarden {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 5, 5 },
            strength = new int[] { 5, 6 },
            health = new int[] { 2, 3 },
            speed = 2,
            range = 4,
            damageType = DamageType.Magical,
            race = Race.Succubus,
            rarity = CardRarity.Common,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.enflame.Add();
        ability.immolate.Add(2, 3);

        return stats;
    }
}