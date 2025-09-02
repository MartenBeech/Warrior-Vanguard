public class DemonicAbomination {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 6, 6 },
            strength = new int[] { 7, 7 },
            health = new int[] { 23, 28 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Demon,
            rarity = CardRarity.Rare,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.bloodPact.Add(2);
        ability.selfHarm.Add();
        ability.vengeance.Add(2, 3);

        return stats;
    }
}