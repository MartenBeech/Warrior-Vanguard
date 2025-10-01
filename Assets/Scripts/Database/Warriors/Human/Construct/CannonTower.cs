public class CannonTower {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 3, 3 },
            strength = new int[] { 3, 4 },
            health = new int[] { 7, 9 },
            speed = 0,
            range = 4,
            damageType = DamageType.Physical,
            race = Race.Construct,
            rarity = CardRarity.Common,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.construct.Add();
        ability.demolish.Add();

        return stats;
    }
}