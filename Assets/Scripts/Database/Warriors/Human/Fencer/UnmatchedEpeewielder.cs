public class UnmatchedEpeewielder {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 3,
            cost = new int[] { 8, 8 },
            strength = new int[] { 2, 2 },
            health = new int[] { 11, 13 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Fencer,
            rarity = CardRarity.Legendary,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.vulnerability.Add(2, 3);
        ability.doubleStrike.Add();
        ability.firstStrike.Add();
        ability.retaliate.Add();

        return stats;
    }
}