using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour {
    private GridManager gridManager;
    private Button button;
    public Vector2 gridIndex;

    public void Setup(GridManager manager, Vector2 gridIndex) {
        gridManager = manager;
        button = GetComponent<Button>();
        button.onClick.AddListener(() => OnClick(gridIndex));
        this.gridIndex = gridIndex;
    }

    void OnClick(Vector2 gridIndex) {
        gridManager.SelectCell(gridIndex);
    }
}