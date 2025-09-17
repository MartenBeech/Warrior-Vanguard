using UnityEngine;

public class MainMenu : MonoBehaviour {
    public GameObject collection;
    public GameObject achievements;
    public void StartNewGame() {
        DeleteTemporaryPlayerPrefs();
        SceneLoader.LoadScene(SceneLoader.Scene.SummonerSelector);
    }

    public void ContinueGame() {
        ItemManager.LoadAvailableItems();
        ContinueManager.LoadSummoner();
        SceneLoader.LoadScene(SceneLoader.Scene.Map);
    }

    public void ExitGame() {
        SceneLoader.ExitGame();
    }

    public void LoadCredits() {
        SceneLoader.LoadScene(SceneLoader.Scene.Credits);
    }

    public void ToggleCollection() {
        collection.SetActive(!collection.activeSelf);
    }

    public void ToggleAchievements() {
        achievements.SetActive(!achievements.activeSelf);
    }

    void DeleteTemporaryPlayerPrefs() {
        //Delete all keys, and reassign the permanent keys afterwards
        int humanExp = ExperienceManager.GetExperience(Genre.Human);
        int elvesExp = ExperienceManager.GetExperience(Genre.Elves);
        int undeadExp = ExperienceManager.GetExperience(Genre.Undead);
        int underworldExp = ExperienceManager.GetExperience(Genre.Underworld);

        int humanLevel = ExperienceManager.GetLevel(Genre.Human);
        int elvesLevel = ExperienceManager.GetLevel(Genre.Elves);
        int undeadLevel = ExperienceManager.GetLevel(Genre.Undead);
        int underworldLevel = ExperienceManager.GetLevel(Genre.Underworld);

        int humanWins = ProgressHelper.GetWins(Genre.Human);
        int elvesWins = ProgressHelper.GetWins(Genre.Elves);
        int undeadWins = ProgressHelper.GetWins(Genre.Undead);
        int underworldWins = ProgressHelper.GetWins(Genre.Underworld);

        int survivedWith1Hp = PlayerPrefs.GetInt(PlayerPrefsKeys.survivedWith1Hp, 0);
        int tenOrMoreFriends = PlayerPrefs.GetInt(PlayerPrefsKeys.tenOrMoreFriends, 0);

        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt(ExperienceManager.ExpKey(Genre.Human), humanExp);
        PlayerPrefs.SetInt(ExperienceManager.ExpKey(Genre.Elves), elvesExp);
        PlayerPrefs.SetInt(ExperienceManager.ExpKey(Genre.Undead), undeadExp);
        PlayerPrefs.SetInt(ExperienceManager.ExpKey(Genre.Underworld), underworldExp);

        PlayerPrefs.SetInt(ExperienceManager.LevelKey(Genre.Human), humanLevel);
        PlayerPrefs.SetInt(ExperienceManager.LevelKey(Genre.Elves), elvesLevel);
        PlayerPrefs.SetInt(ExperienceManager.LevelKey(Genre.Undead), undeadLevel);
        PlayerPrefs.SetInt(ExperienceManager.LevelKey(Genre.Underworld), underworldLevel);

        PlayerPrefs.SetInt(ProgressHelper.WinsKey(Genre.Human), humanWins);
        PlayerPrefs.SetInt(ProgressHelper.WinsKey(Genre.Elves), elvesWins);
        PlayerPrefs.SetInt(ProgressHelper.WinsKey(Genre.Undead), undeadWins);
        PlayerPrefs.SetInt(ProgressHelper.WinsKey(Genre.Underworld), underworldWins);

        PlayerPrefs.SetInt(PlayerPrefsKeys.survivedWith1Hp, survivedWith1Hp);
        PlayerPrefs.SetInt(PlayerPrefsKeys.tenOrMoreFriends, tenOrMoreFriends);
        PlayerPrefs.Save();
    }
}
