public class TheOriginal {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 8, 8 },
            strength = new int[] { 3, 4 },
            health = new int[] { 13, 16 },
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Vampire,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }
        stats.rarity = CardRarity.Rare;

        WarriorAbility ability = stats.ability;
        ability.lifeSteal.Add();
        ability.retaliate.Add();
        ability.bleed.Add();

        return stats;
    }
}