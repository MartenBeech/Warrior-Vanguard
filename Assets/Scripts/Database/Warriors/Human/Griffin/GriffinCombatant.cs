public class GriffinCombatant {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 6, 6 },
            strength = new int[] { 3, 3 },
            health = new int[] { 9, 10 },
            speed = 4,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Griffin,
            rarity = CardRarity.Rare,
            genre = Character.Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.flying.Add();
        ability.retaliate.Add();
        ability.vengeance.Add(1, 2);

        return stats;
    }
}