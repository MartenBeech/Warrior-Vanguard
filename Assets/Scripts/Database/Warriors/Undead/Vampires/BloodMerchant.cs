public class BloodMerchant {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = 4,
            strength = new int[] { 4, 5 },
            health = new int[] { 6, 8 },
            speed = 2,
            range = 4,
            damageType = Character.DamageType.Physical,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }
        stats.rarity = CardRarity.Rare;

        WarriorAbility ability = stats.ability;
        ability.lifeTransfer.Add();

        return stats;
    }
}