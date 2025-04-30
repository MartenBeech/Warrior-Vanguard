public class VampireRing : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All friendly vampires gain 2 health";
        return this;
    }

    public override void UseOnFriendSpawn(WarriorStats stats) {
        if (stats.race == Character.Race.Vampire) {
            stats.AddHealthMax(2);
        }
    }
}