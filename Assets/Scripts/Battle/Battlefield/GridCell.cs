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

    public void HighlightDeployableCells()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < gridManager.rows; y++)
            {
                gridManager.HighlightCell(x, y);
            }
        }
    }

    public void ClearHighlightedDeployableCells()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < gridManager.rows; y++)
            {
                gridManager.ClearHighlightedCell(x, y);
            }
        }
    }
}