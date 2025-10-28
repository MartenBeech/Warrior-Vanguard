public class BranchManager {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 4, 4 },
            strength = new int[] { 2, 2 },
            health = new int[] { 9, 9 },
            speed = 1,
            range = 1,
            damageType = DamageType.Physical,
            race = Race.Treant,
            rarity = CardRarity.Common,
            genre = Genre.Elves
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.rooting.Add();

        return stats;
    }
}