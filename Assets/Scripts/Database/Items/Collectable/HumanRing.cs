public class HumanRing : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All friendly humans gain 2 health";
        return this;
    }

    public override void UseOnFriendSpawn(WarriorStats stats) {
        if (stats.race == Character.Race.Human) {
            stats.AddHealth(2);
        }
    }
}