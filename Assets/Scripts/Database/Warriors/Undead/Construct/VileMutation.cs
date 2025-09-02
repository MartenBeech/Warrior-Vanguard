public class VileMutation {

    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 7, 7 },
            strength = new int[] { 0, 0 },
            health = new int[] { 9, 11 },
            speed = 0,
            range = 0,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Construct,
            rarity = CardRarity.Rare,
            genre = Warrior.Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.construct.Add();
        ability.poisonCloud.Add(3, 4);
        ability.poisoningAura.Add(3, 4);

        return stats;
    }
}