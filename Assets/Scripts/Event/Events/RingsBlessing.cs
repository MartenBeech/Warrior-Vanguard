using UnityEngine;
public class RingsBlessing {
    public Event GetEvent(EventManager eventManager) {
        string raceKey = "RingsBlessingRaceKey";
        Warrior.Race race1 = Rng.GetRandomRace(FriendlySummoner.summonerData.genre, true);
        Warrior.Race race2 = Rng.GetRandomRace(FriendlySummoner.summonerData.genre, true);
        Warrior.Race race3 = Rng.GetRandomRace(FriendlySummoner.summonerData.genre, true);
        Item ringOfRace = ItemManager.GetItemByTitle("RingOfRace");

        // Ensure we don't get duplicates
        while (race2 == race1) {
            race2 = Rng.GetRandomRace(FriendlySummoner.summonerData.genre, true);
        }
        while (race3 == race1 || race3 == race2) {
            race3 = Rng.GetRandomRace(FriendlySummoner.summonerData.genre, true);
        }

        Event newEvent = new() {

            OnSetup = () => {
                eventManager.eventText.text = "You find a ring infused with a shiny crystal. It seems to constantly change color. Until you put it on";
                eventManager.option3Text.text = $"Gain ring of {race3}";
                eventManager.option2Text.text = $"Gain ring of {race2}";
                eventManager.option1Text.text = $"Gain ring of {race1}";
            },

            OnClickOption3 = () => {
                eventManager.eventText.text = $"Every {race3} you own gets a bit stronger";
                PlayerPrefs.SetString(raceKey, race3.ToString());
                PlayerPrefs.Save();
                ItemManager.AddItem(ringOfRace);
            },

            OnClickOption2 = () => {
                eventManager.eventText.text = $"Every {race2} you own gets a bit stronger";
                PlayerPrefs.SetString(raceKey, race2.ToString());
                PlayerPrefs.Save();
                ItemManager.AddItem(ringOfRace);
            },

            OnClickOption1 = () => {
                eventManager.eventText.text = $"Every {race1} you own gets a bit stronger";
                PlayerPrefs.SetString(raceKey, race1.ToString());
                PlayerPrefs.Save();
                ItemManager.AddItem(ringOfRace);
            },
        };

        return newEvent;
    }
}