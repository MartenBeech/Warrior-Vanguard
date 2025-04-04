using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour {
    private GridManager gridManager;
    private Button button;
    public Vector2 gridIndex;
    bool isHighlighed;

    public void Setup(GridManager manager, Vector2 gridIndex) {
        gridManager = manager;
        button = GetComponent<Button>();
        this.gridIndex = gridIndex;
        button.onClick.AddListener(async () => await OnClick());
    }

    public async Task<bool> OnClick() {
        return await gridManager.SelectCell(gridIndex);
    }

    public void Highlight() {
        GetComponent<Outline>().enabled = true;
        GetComponent<Outline>().effectColor = ColorPalette.GetColor(ColorPalette.ColorEnum.teal);
        GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.tealWeak);
        isHighlighed = true;
        Character character = gridManager.GetCellCharacter(gridIndex);
        if (character) {
            character.image.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.tealWeak);
        }
    }

    public void ClearHighlight() {
        GetComponent<Outline>().enabled = false;
        GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.white);
        isHighlighed = false;
        Character character = gridManager.GetCellCharacter(gridIndex);
        if (character) {
            character.image.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.white);
        }
    }

    public bool IsHighlighed() {
        return isHighlighed;
    }
}