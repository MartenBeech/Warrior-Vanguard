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
        this.gridIndex = gridIndex;
        button.onClick.AddListener(async () => await OnClick());
    }

    public async Task OnClick() {
        await gridManager.SelectCell(gridIndex);
    }
}