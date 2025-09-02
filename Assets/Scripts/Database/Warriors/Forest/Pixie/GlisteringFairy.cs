public class GlisteringFairy {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 4 },
            strength = new int[] { 0, 0 },
            health = new int[] { 6, 7 },
            speed = 2,
            range = 2,
            damageType = DamageType.Magical,
            race = Race.Pixie,
            rarity = CardRarity.Rare,
            genre = Genre.Forest,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.flying.Add();
        ability.faeMagic.Add(2, 3);
        ability.sapPower.Add();

        return stats;
    }
}