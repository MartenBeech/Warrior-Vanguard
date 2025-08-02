using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class DragonRecruiter {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Kill}: Add a random dragon to your hand";
    }

    public async Task<bool> TriggerKill(Character dealer, Hand hand) {
        if (GetValue(dealer.stats)) {
            WarriorStats randomDragon = CardDatabase.GetRandomWarriorWithSpecificRace(Character.Race.Dragon);
            randomDragon.alignment = dealer.stats.alignment;

            await hand.MoveNewCardToHand(randomDragon, dealer.transform.position);
            return true;
        }
        return false;
    }

    bool[] value = new bool[] { false, false };

    public bool GetValue(WarriorStats stats) {
        return value[stats.level];
    }

    public void Add(bool unupgradedValue, bool upgradedValue) {
        bool[] newValues = new bool[] { unupgradedValue, upgradedValue };
        for (int i = 0; i < 2; i++) {
            value[i] = newValues[i];
        }
    }

    public void Add() {
        Add(true, true);
    }

    public void Remove() {
        Add(false, false);
    }

    public string GetTitle(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{GetAbilityName()}\n";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}