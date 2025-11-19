public class JudgementCaller {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 4, 4 },
            strength = new int[] { 2, 2 },
            health = new int[] { 5, 7 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Reaper,
            rarity = CardRarity.Common,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.darkTouch.Add(4, 6);

        return stats;
    }
}