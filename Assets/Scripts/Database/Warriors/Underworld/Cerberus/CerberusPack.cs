public class CerberusPack {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 7, 7 },
            strength = new int[] { 3, 4 },
            health = new int[] { 3, 4 },
            speed = 3,
            range = 2,
            damageType = DamageType.Magical,
            race = Race.Cerberus,
            rarity = CardRarity.Rare,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.cleave.Add();
        ability.enflame.Add();
        ability.spawn.Add(2);

        return stats;
    }
}