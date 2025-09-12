using UnityEngine;

public class MainMenu : MonoBehaviour {
    public GameObject collection;
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

    void DeleteTemporaryPlayerPrefs() {
        //Delete all keys, and reassign the permanent keys afterwards
        int humanExp = ExperienceManager.GetExperience(Genre.Human);
        int humanLevel = ExperienceManager.GetLevel(Genre.Human);
        int elvesExp = ExperienceManager.GetExperience(Genre.Elves);
        int elvesLevel = ExperienceManager.GetLevel(Genre.Elves);
        int undeadExp = ExperienceManager.GetExperience(Genre.Undead);
        int undeadLevel = ExperienceManager.GetLevel(Genre.Undead);
        int underworldExp = ExperienceManager.GetExperience(Genre.Underworld);
        int underworldLevel = ExperienceManager.GetLevel(Genre.Underworld);
        int humanWins = ExperienceManager.GetWins(Genre.Human);
        int elvesWins = ExperienceManager.GetWins(Genre.Elves);
        int undeadWins = ExperienceManager.GetWins(Genre.Undead);
        int underworldWins = ExperienceManager.GetWins(Genre.Underworld);

        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt(ExperienceManager.ExpKey(Genre.Human), humanExp);
        PlayerPrefs.SetInt(ExperienceManager.ExpKey(Genre.Elves), elvesExp);
        PlayerPrefs.SetInt(ExperienceManager.ExpKey(Genre.Undead), undeadExp);
        PlayerPrefs.SetInt(ExperienceManager.ExpKey(Genre.Underworld), underworldExp);
        
        PlayerPrefs.SetInt(ExperienceManager.LevelKey(Genre.Human), humanLevel);
        PlayerPrefs.SetInt(ExperienceManager.LevelKey(Genre.Elves), elvesLevel);
        PlayerPrefs.SetInt(ExperienceManager.LevelKey(Genre.Undead), undeadLevel);
        PlayerPrefs.SetInt(ExperienceManager.LevelKey(Genre.Underworld), underworldLevel);

        PlayerPrefs.SetInt(ExperienceManager.WinsKey(Genre.Human), humanWins);
        PlayerPrefs.SetInt(ExperienceManager.WinsKey(Genre.Elves), elvesWins);
        PlayerPrefs.SetInt(ExperienceManager.WinsKey(Genre.Undead), undeadWins);
        PlayerPrefs.SetInt(ExperienceManager.WinsKey(Genre.Underworld), underworldWins);
        PlayerPrefs.Save();
    }
}
