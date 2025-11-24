public class ArtProfessorArnold {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 6, 5 },
            strength = new int[] { 3, 3 },
            health = new int[] { 8, 8 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Support,
            rarity = CardRarity.Legendary,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.artist.Add(1);

        return stats;
    }
}