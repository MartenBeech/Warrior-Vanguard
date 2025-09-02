public class PoiSonRogue {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            strength = new int[] { 3, 3 },
            health = new int[] { 2, 3 },
            speed = 2,
            range = 2,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Human,
            rarity = CardRarity.Common,
            genre = Warrior.Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.poison.Add(1, 2);
        ability.backstab.Add();

        return stats;
    }
}