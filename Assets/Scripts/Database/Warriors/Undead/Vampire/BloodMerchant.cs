public class BloodMerchant {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 4 },
            strength = new int[] { 4, 5 },
            health = new int[] { 6, 8 },
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Vampire,
            genre = Character.Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }
        stats.rarity = CardRarity.Rare;

        WarriorAbility ability = stats.ability;
        ability.lifeTransfer.Add();
        ability.bleed.Add();

        return stats;
    }
}