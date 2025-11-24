public class BarrageCannoneer {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 8, 8 },
            strength = new int[] { 6, 8 },
            health = new int[] { 6, 8 },
            speed = 2,
            range = 5,
            damageType = DamageType.Physical,
            race = Race.Marksman,
            rarity = CardRarity.Legendary,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.reload.Add();
        ability.bash.Add();
        ability.splash.Add();
        ability.demolish.Add();

        return stats;
    }
}