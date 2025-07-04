public class CerberusPack {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 7, 7 },
            strength = new int[] { 2, 2 },
            health = new int[] { 3, 4 },
            speed = 3,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Cerberus,
            rarity = CardRarity.Rare,
            genre = Character.Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.cleave.Add();
        ability.enflame.Add(2, 3);
        ability.spawn.Add(2);

        return stats;
    }
}