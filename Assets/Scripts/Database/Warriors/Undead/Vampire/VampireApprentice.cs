public class VampireApprentice {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            strength = new int[] { 1, 2 },
            health = new int[] { 5, 6 },
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Vampire,
            rarity = CardRarity.Common,
            genre = Character.Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }
        stats.rarity = CardRarity.Rare;

        WarriorAbility ability = stats.ability;
        ability.lifeSteal.Add();
        ability.bleed.Add();

        return stats;
    }
}