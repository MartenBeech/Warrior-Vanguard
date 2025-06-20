public class ClubCrasher {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 4 },
            strength = new int[] { 3, 4 },
            health = new int[] { 6, 7 },
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Troll,
            rarity = CardRarity.Common,
            genre = Character.Genre.Forest
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.stoneskin.Add();
        ability.bash.Add();

        return stats;
    }
}