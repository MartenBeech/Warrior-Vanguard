public class CorruptedSprite {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 3 },
            strength = new int[] { 0, 0 },
            health = new int[] { 5, 6 },
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Magical,
            race = Character.Race.Pixie,
            rarity = CardRarity.Legendary,
            genre = Character.Genre.Forest,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.flying.Add();
        ability.sapPower.Add();
        ability.weaken.Add(2, 3);

        return stats;
    }
}