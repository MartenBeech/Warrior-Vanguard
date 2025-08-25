using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

public class SummonFlotSam {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.StartOfCombat}: Summon a 2/3 Flot Sam";
    }

    public async Task<bool> TriggerStartOfCombat(Summoner dealer, CharacterSpawner characterSpawner) {
        WarriorStats flotSam = new FlotSam().GetStats();
        flotSam.alignment = characterSpawner.spawningAlignment;
        flotSam.level = 0;
        flotSam.SetStats(flotSam);

        await characterSpawner.SpawnRandomly(flotSam, dealer.transform.position);
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

    public WarriorAbility.BuffType buffType = WarriorAbility.BuffType.None;
}