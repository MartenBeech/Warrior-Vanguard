public class ShinyButterfly {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 0, 0 },
            strength = new int[] { 0, 0 },
            health = new int[] { 3, 3 },
            speed = 2,
            range = 2,
            damageType = DamageType.Magical,
            race = Race.Pixie,
            rarity = CardRarity.Common,
            genre = Genre.Elves
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.flying.Add();
        ability.faeMagic.Add(1, 2);

        return stats;
    }
}