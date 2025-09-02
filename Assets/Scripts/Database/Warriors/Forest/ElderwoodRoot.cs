public class ElderwoodRoot {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 1, 1 },
            strength = new int[] { 1, 1 },
            health = new int[] { 3, 4 },
            speed = 0,
            range = 1,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Treant,
            rarity = CardRarity.Common,
            genre = Warrior.Genre.Forest,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.rooting.Add();
        ability.sapEnergy.Add(1, 2);

        return stats;
    }
}