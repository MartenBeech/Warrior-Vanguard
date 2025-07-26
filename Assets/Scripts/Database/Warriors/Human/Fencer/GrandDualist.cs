public class GrandDualist {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 5, 5 },
            strength = new int[] { 1, 1 },
            health = new int[] { 10, 11 },
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Fencer,
            rarity = CardRarity.Rare,
            genre = Character.Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.vulnerability.Add(1);
        ability.doubleStrike.Add();
        ability.bloodlust.Add(1, 2);

        return stats;
    }
}