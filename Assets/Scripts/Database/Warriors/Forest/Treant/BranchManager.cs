public class BranchManager {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 4 },
            strength = new int[] { 2, 2 },
            health = new int[] { 9, 9 },
            speed = 1,
            range = 1,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Treant,
            rarity = CardRarity.Common,
            genre = Warrior.Genre.Forest
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.rooting.Add();

        return stats;
    }
}