public class DreadKnight {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 3 },
            strength = new int[] { 2, 3 },
            health = new int[] { 6, 6 },
            speed = 4,
            range = 2,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Nightrider,
            rarity = CardRarity.Common,
            genre = Warrior.Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.cheatDeath.Add();
        ability.armor.Add(1, 2);

        return stats;
    }
}