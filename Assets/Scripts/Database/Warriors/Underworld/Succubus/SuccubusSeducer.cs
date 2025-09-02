public class SuccubusSeducer {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 6, 6 },
            strength = new int[] { 4, 5 },
            health = new int[] { 5, 7 },
            speed = 2,
            range = 4,
            damageType = DamageType.Magical,
            race = Race.Succubus,
            rarity = CardRarity.Rare,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.enflame.Add();
        ability.immolate.Add(1, 2);
        ability.seduce.Add();

        return stats;
    }
}