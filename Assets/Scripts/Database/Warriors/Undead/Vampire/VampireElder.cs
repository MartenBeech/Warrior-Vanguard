public class VampireElder {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 6, 6 },
            strength = new int[] { 5, 7 },
            health = new int[] { 8, 8 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Vampire,
            rarity = CardRarity.Common,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.lifeSteal.Add();
        ability.bleed.Add();

        return stats;
    }
}