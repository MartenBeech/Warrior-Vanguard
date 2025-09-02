public class ShadowyEntity {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 10, 10 },
            strength = new int[] { 4, 5 },
            health = new int[] { 8, 10 },
            speed = 3,
            range = 2,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Ghoul,
            rarity = CardRarity.Legendary,
            genre = Warrior.Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.cannibalism.Add(4, 5);
        ability.stealth.Add();
        ability.permaStealth.Add();

        return stats;
    }
}