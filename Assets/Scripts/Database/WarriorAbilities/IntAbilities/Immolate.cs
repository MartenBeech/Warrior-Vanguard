using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Immolate {
    public string GetDescription(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        return $"{WarriorAbility.Keywords.Overturn}: Deal {GetValue(stats)} damage to the enemy summoner";
    }

    public async Task<bool> Trigger(Character dealer, GridManager gridManager, GameManager gameManager) {
        if (GetValue(dealer.stats) > 0) {
            Summoner summonerTarget = dealer.stats.alignment == CharacterSpawner.Alignment.Enemy ?
                    gameManager.friendSummonerObject.GetComponent<Summoner>() :
                    gameManager.enemySummonerObject.GetComponent<Summoner>();
            await summonerTarget.TakeDamage(dealer, GetValue(dealer.stats), gridManager);
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
}