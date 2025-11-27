public class Archmagi {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 8, 8 },
            strength = new int[] { 5, 6 },
            health = new int[] { 7, 10 },
            speed = 2,
            range = 4,
            damageType = DamageType.Magical,
            race = Race.Druid,
            rarity = CardRarity.Legendary,
            genre = Genre.Elves,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.swap.Add();
        ability.forestFriend.Add();
        ability.turnSwap.Add();

        return stats;
    }
}