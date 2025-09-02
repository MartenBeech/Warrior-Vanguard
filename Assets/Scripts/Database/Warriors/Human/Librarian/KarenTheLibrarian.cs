public class KarenTheLibrarian {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 9, 9 },
            strength = new int[] { 1, 2 },
            health = new int[] { 6, 7 },
            speed = 2,
            range = 2,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Librarian,
            rarity = CardRarity.Legendary,
            genre = Warrior.Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.enlighten.Add(3, 4);
        ability.intelligence.Add(1, 2);
        ability.massSilence.Add();

        return stats;
    }
}