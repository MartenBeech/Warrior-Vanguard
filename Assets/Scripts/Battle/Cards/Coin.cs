using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour {
    public GameObject coinPrefab;
    int coins = 0;
    int coinsTotal = 0;
    public Hand hand;

    public void GainCoins(int amount = 1) {
        for (int i = 0; i < amount; i++) {
            if (coinsTotal >= 10) break;
            Instantiate(coinPrefab, transform);
        }
        coinsTotal += amount;
        if (coinsTotal > 10) {
            coinsTotal = 10;
        }
        coins += amount;
        if (coinsTotal > 10) {
            coins = 10;
        }

        hand.UpdateDisabledCardsUI();
    }

    public bool SpendCoins(int amount) {
        if (CanAfford(amount)) {
            for (int i = coins - amount; i < coins; i++) {
                GameObject child = transform.GetChild(i).gameObject;
                child.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.gray);
            }
            coins -= amount;

            hand.UpdateDisabledCardsUI();

            return true;
        }
        return false;
    }

    public void RefreshCoins() {
        for (int i = 0; i < coinsTotal; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            child.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.yellow);
        }
        coins = coinsTotal;

        hand.UpdateDisabledCardsUI();
    }

    public bool CanAfford(int cost) {
        if (cost <= coins) {
            return true;
        }
        return false;
    }
}