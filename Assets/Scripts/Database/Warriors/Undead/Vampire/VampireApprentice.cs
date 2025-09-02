public class VampireApprentice {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            strength = new int[] { 1, 2 },
            health = new int[] { 4, 4 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Vampire,
            rarity = CardRarity.Common,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.lifeSteal.Add();
        ability.bleed.Add();

        return stats;
    }
}