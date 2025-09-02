public class FieryAvatar {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 9, 9 },
            strength = new int[] { 6, 8 },
            health = new int[] { 10, 12 },
            speed = 2,
            range = 4,
            damageType = Warrior.DamageType.Magical,
            race = Warrior.Race.Succubus,
            rarity = CardRarity.Legendary,
            genre = Warrior.Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.enflame.Add();
        ability.immolate.Add(1, 3);
        ability.massEnflame.Add();
        ability.massImmolate.Add(1);

        return stats;
    }
}