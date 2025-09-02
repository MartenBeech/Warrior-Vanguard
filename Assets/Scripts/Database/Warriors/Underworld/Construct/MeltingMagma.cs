public class MeltingMagma {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 7, 7 },
            strength = new int[] { 0, 0 },
            health = new int[] { 21, 21 },
            speed = 0,
            range = 0,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Construct,
            rarity = CardRarity.Legendary,
            genre = Warrior.Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.construct.Add();
        ability.immolate.Add(2, 3);
        ability.burning.Add(6, 5);
        ability.firewall.Add(3, 4);

        return stats;
    }
}