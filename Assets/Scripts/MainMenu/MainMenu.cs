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

        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt(ExperienceManager.ExpKey(Genre.Human), humanExp);
        PlayerPrefs.SetInt(ExperienceManager.LevelKey(Genre.Human), humanLevel);
        PlayerPrefs.SetInt(ExperienceManager.ExpKey(Genre.Elves), elvesExp);
        PlayerPrefs.SetInt(ExperienceManager.LevelKey(Genre.Elves), elvesLevel);
        PlayerPrefs.SetInt(ExperienceManager.ExpKey(Genre.Undead), undeadExp);
        PlayerPrefs.SetInt(ExperienceManager.LevelKey(Genre.Undead), undeadLevel);
        PlayerPrefs.SetInt(ExperienceManager.ExpKey(Genre.Underworld), underworldExp);
        PlayerPrefs.SetInt(ExperienceManager.LevelKey(Genre.Underworld), underworldLevel);
        PlayerPrefs.Save();
    }
}
