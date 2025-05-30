public class VampireApprentice {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            strength = new int[] { 1, 1 },
            health = new int[] { 5, 7 },
            speed = 2,
            range = 4,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Vampire,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }
        stats.rarity = CardRarity.Rare;

        WarriorAbility ability = stats.ability;
        ability.lifeSteal.Add();
        ability.weaken.Add(1, 2);

        return stats;
    }
}