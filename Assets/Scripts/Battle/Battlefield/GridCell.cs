using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour {
    private GridManager gridManager;
    private Button button;
    public Vector2 gridIndex;

    public void Setup(GridManager manager, Vector2 gridIndex) {
        gridManager = manager;
        button = GetComponent<Button>();
        button.onClick.AddListener(async () => await OnClick(gridIndex));
        this.gridIndex = gridIndex;
    }

    async Task OnClick(Vector2 gridIndex) {
        await gridManager.SelectCell(gridIndex);
    }
}