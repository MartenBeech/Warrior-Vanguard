public class NightHunter {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 3 },
            strength = new int[] { 1, 1 },
            health = new int[] { 6, 8 },
            speed = 3,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Werewolf,
            rarity = CardRarity.Common,
            genre = Character.Genre.Forest,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.carnivore.Add();
        ability.bloodlust.Add(1);

        return stats;
    }
}