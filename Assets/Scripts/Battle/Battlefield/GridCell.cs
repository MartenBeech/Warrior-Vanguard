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
        isHighlighed = true;

        Character character = gridManager.GetCellCharacter(gridIndex);
        if (character) {
            float currentTransparency = character.image.GetComponent<Image>().color.a;
            Color newColor = ColorPalette.GetColor(ColorPalette.ColorEnum.TealMedium);
            newColor.a = currentTransparency;
            character.image.GetComponent<Image>().color = newColor;
        }

        GetComponent<Outline>().enabled = true;
        GetComponent<Outline>().effectColor = ColorPalette.GetColor(ColorPalette.ColorEnum.Teal);
        GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.TealWeak);
    }

    public void ClearHighlight() {
        isHighlighed = false;

        Character character = gridManager.GetCellCharacter(gridIndex);
        if (character) {
            float currentTransparency = character.image.GetComponent<Image>().color.a;
            Color newColor = ColorPalette.GetColor(ColorPalette.ColorEnum.White);
            newColor.a = currentTransparency;
            character.image.GetComponent<Image>().color = newColor;
        }

        GetComponent<Outline>().enabled = false;

        GetComponent<Image>().color = ColorPalette.AddTransparency(ColorPalette.GetColor(ColorPalette.ColorEnum.White), 10);
    }

    public bool IsHighlighed() {
        return isHighlighed;
    }
}