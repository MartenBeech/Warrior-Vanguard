public class UprootedWoods {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 6, 6 },
            strength = new int[] { 1, 1 },
            health = new int[] { 8, 10 },
            speed = 1,
            range = 1,
            damageType = DamageType.Physical,
            race = Race.Treant,
            rarity = CardRarity.Rare,
            genre = Genre.Elves
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.familiarGround.Add(4, 5);
        ability.rooting.Add();
        ability.guard.Add();

        return stats;
    }
}