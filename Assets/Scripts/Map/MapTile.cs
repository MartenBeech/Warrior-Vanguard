using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTile : MonoBehaviour
{
    public void GoToBattlefield()
    {
        SceneManager.LoadScene("Battlefield");
    }
}
