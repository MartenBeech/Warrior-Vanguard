using System.Collections.Generic;
using UnityEngine;

public class Rng : MonoBehaviour {


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
        int randomIndex = Range(0, list.Count);

        T randomEntry = list[randomIndex];
        return randomEntry;
    }
}