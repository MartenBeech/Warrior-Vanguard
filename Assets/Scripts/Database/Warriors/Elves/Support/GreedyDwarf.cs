public class GreedyDwarf {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 4, 4 },
            strength = new int[] { 4, 5 },
            health = new int[] { 4, 6 },
            speed = 1,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Support,
            rarity = CardRarity.Rare,
            genre = Genre.Elves,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.bash.Add();
        ability.greedyStrike.Add(25, 50);
        ability.drawing.Add(1);

        return stats;
    }
}