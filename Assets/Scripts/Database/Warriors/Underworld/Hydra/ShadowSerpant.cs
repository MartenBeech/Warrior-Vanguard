public class ShadowSerpant {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 4, 4 },
            strength = new int[] { 2, 3 },
            health = new int[] { 6, 6 },
            speed = 2,
            range = 1,
            damageType = DamageType.Physical,
            race = Race.Hydra,
            rarity = CardRarity.Common,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        //TODO: Stealth double damage only triggers on one enemy, because it strikes in a loop, and loses stealth after first strike. We should consider if it's worth trying to fix it.
        ability.whirlwind.Add();
        ability.stealth.Add();

        return stats;
    }
}