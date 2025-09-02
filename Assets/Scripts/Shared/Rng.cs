using System.Collections.Generic;
using UnityEngine;

public class Rng : MonoBehaviour {

    private static string raceKey = "raceKey";
    public static int Range(int min, int maxExclusive) {
        List<int> numbers = new();

        const int rngFactor = 1000;

        min *= rngFactor;
        maxExclusive *= rngFactor;

        for (int i = 0; i < 10; i++) {
            numbers.Add(Random.Range(min, maxExclusive));
        }

        float number = numbers[Random.Range(0, numbers.Count)];
        float result = number / rngFactor;

        return Mathf.FloorToInt(result);
    }

    public static bool Chance(float percentageOfSuccess) {
        if (percentageOfSuccess <= 0) {
            return false;
        }
        if (percentageOfSuccess >= 100) {
            return true;
        }

        int numberRolled = Range(1, 101);
        if (numberRolled < Mathf.RoundToInt(percentageOfSuccess)) {
            return true;
        }
        return false;
    }

    public static T Entry<T>(List<T> list) {
        if (list.Count == 0) return default;
        int randomIndex = Range(0, list.Count);

        T randomEntry = list[randomIndex];
        return randomEntry;
    }

    // The race will be saved for the rest of the game
    public static Race GetRandomRace(Genre genre, bool forceNew = false) {
        List<WarriorStats> warriors = CardDatabase.allCards.FindAll(card => card.genre == genre);

        var uniqueRaces = new HashSet<Race>();
        foreach (var warrior in warriors) {
            if (warrior.race == Race.None) continue;
            uniqueRaces.Add(warrior.race);
        }
        if (uniqueRaces.Count == 0)
            return default;

        var raceList = new List<Race>(uniqueRaces);
        int randomIndex;
        if (PlayerPrefs.HasKey(raceKey) && !forceNew) {
            randomIndex = PlayerPrefs.GetInt(raceKey);
        } else {
            randomIndex = Random.Range(0, raceList.Count);
        }

        PlayerPrefs.SetInt(raceKey, randomIndex);
        PlayerPrefs.Save();

        return raceList[randomIndex];
    }
}