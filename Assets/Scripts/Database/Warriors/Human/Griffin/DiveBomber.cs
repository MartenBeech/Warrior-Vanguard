public class DiveBomber {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 3 },
            strength = new int[] { 3, 4 },
            health = new int[] { 3, 4 },
            speed = 4,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Griffin,
            rarity = CardRarity.Legendary,
            genre = Character.Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.flying.Add();
        ability.stealth.Add();
        ability.firstStrike.Add();

        return stats;
    }
}