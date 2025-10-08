public class VoidBeing {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 7, 7 },
            strength = new int[] { 5, 6 },
            health = new int[] { 4, 5 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Wraith,
            rarity = CardRarity.Rare,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.afterlife.Add();
        ability.incorporeal.Add();
        ability.darkTouch.Add(5, 6);

        return stats;
    }
}