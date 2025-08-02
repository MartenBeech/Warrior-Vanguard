using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

public class SummonWisp {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Overturn}: Summon a 1/1 Wisp";
    }

    public async Task<bool> TriggerOverturn(Summoner dealer, CharacterSpawner characterSpawner) {
            WarriorStats wisp = new Wisp().GetStats();
            wisp.alignment = characterSpawner.spawningAlignment;
            wisp.level = 0;
            wisp.SetStats(wisp);

            await characterSpawner.SpawnRandomly(wisp, dealer.transform.position);
            return true;
    }

    bool[] value = new bool[] { false, false };

    public bool GetValue(WarriorStats stats) {
        return value[stats.level];
    }

    public bool GetValue(SummonerStats stats) {
        return value[0];
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