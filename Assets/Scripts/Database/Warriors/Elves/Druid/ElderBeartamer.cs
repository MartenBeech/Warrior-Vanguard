public class ElderBeartamer {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 4, 4 },
            strength = new int[] { 2, 3 },
            health = new int[] { 7, 9 },
            speed = 2,
            range = 4,
            damageType = DamageType.Magical,
            race = Race.Druid,
            rarity = CardRarity.Common,
            genre = Genre.Elves,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.swap.Add();
        ability.forestFriend.Add();

        return stats;
    }
}