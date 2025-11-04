using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class StrengthenByFireAbility {
    public string GetDescription(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        return $"{Keyword.Overturn}: take 1 damage and gain {GetValue(stats)} strength";
    }

    public async Task<bool> TriggerOverturn(Warrior target) {
        if (GetValue(target.stats) > 0) {
            await target.TakeDamage(target, 1, DamageType.Magical, DamageSource.Burning);
            target.stats.AddStrength(GetValue(target.stats));
            target.UpdateWarriorUI();
            return true;
        }
        return false;
    }

    int[] value = new int[] { 0, 0 };

    public int GetValue(WarriorStats stats) {
        return value[stats.level];
    }

    public void Add(int unupgradedValue, int upgradedValue) {
        int[] newValues = new int[] { unupgradedValue, upgradedValue };
        for (int i = 0; i < 2; i++) {
            value[i] += newValues[i];
            if (value[i] < 0) {
                value[i] = 0;
            }
        }
    }

    public void Add(int value) {
        Add(value, value);
    }

   public void Remove() {
        for (int i = 0; i < 2; i++) {
            value[i] = 0;
        }
    }

    public string GetTitle(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        return $"{GetAbilityName()}: {GetValue(stats)}\n";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }

    public BuffType buffType = BuffType.Debuff;
}