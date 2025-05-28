public class UprootedWoods {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 6, 6 },
            strength = new int[] { 2, 2 },
            health = new int[] { 8, 10 },
            speed = 1,
            range = 1,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Treant,
            rarity = CardRarity.Rare,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.familiarGround.Add(4, 5);
        ability.rooting.Add();

        return stats;
    }
}