public class FieryAvatar {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 9, 9 },
            strength = new int[] { 0, 0 },
            health = new int[] { 9, 11 },
            speed = 2,
            range = 4,
            damageType = Character.DamageType.Magical,
            race = Character.Race.Succubus,
            rarity = CardRarity.Legendary,
            genre = Character.Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.enflame.Add(6, 8);
        ability.immolate.Add(2, 3);
        ability.massEnflame.Add(1, 2);
        ability.massImmolate.Add(1);

        return stats;
    }
}