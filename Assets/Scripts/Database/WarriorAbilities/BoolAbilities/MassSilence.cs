using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class MassSilence {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Summon}: Remove all abilities from ALL other warriors";
    }

    public async Task<bool> Trigger(Character dealer, GridManager gridManager, FloatingText floatingText) {
        if (GetValue(dealer.stats)) {
            List<Character> warriors = gridManager.GetCharacters();
            warriors.Remove(dealer);

            List<Task> asyncFunctions = new();
            foreach (var warrior in warriors) {
                warrior.stats.ability = new();
                warrior.UpdateWarriorUI();
                asyncFunctions.Add(floatingText.CreateFloatingText(warrior.transform, "Sileced", ColorPalette.ColorEnum.purple));
            }
            await Task.WhenAll(asyncFunctions);

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