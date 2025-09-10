public class TrollKing {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 9, 9 },
            strength = new int[] { 7, 8 },
            health = new int[] { 7, 8 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Troll,
            rarity = CardRarity.Legendary,
            genre = Genre.Elves
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.stoneskin.Add();
        ability.regeneration.Add(3, 4);
        ability.bash.Add();

        return stats;
    }
}