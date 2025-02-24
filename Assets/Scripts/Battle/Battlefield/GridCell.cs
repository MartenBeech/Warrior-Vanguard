using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour
{
    private int x, y;
    private GridManager gridManager;
    private Button button;

    public void Setup(int x, int y, GridManager manager)
    {
        this.x = x;
        this.y = y;
        gridManager = manager;
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        gridManager.SelectCell(x, y);
    }
}