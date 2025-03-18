using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour {
    public GameObject coinPrefab;
    int coins = 0;
    int coinsTotal = 0;
    ColorPalette colorPalette = new();

    public void GainCoins(int amount = 1) {
        for (int i = 0; i < amount; i++) {
            if (coinsTotal >= 10) break;
            Instantiate(coinPrefab, transform);
        }
        coinsTotal += amount;
        coins += amount;
    }

    public bool SpendCoins(int amount) {
        if (amount <= coins) {
            for (int i = coins - amount; i < coins; i++) {
                GameObject child = transform.GetChild(i).gameObject;
                child.GetComponent<Image>().color = colorPalette.GetColor(ColorPalette.ColorEnum.gray);
            }
            coins -= amount;
            return true;
        }
        return false;
    }

    public void RefreshCoins() {
        for (int i = 0; i < coinsTotal; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            child.GetComponent<Image>().color = colorPalette.GetColor(ColorPalette.ColorEnum.yellow);
        }
        coins = coinsTotal;
    }
}