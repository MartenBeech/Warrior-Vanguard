using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour {
    private GridManager gridManager;
    private Button button;
    public Vector2 gridIndex;
    public GameObject image;
    public GameObject border;
    bool isHighlighed;

    public void Setup(GridManager manager, Vector2 gridIndex) {
        gridManager = manager;
        button = image.GetComponent<Button>();
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
            character.border.SetActive(true);
        }

        border.SetActive(true);
    }

    public void ClearHighlight() {
        isHighlighed = false;

        Character character = gridManager.GetCellCharacter(gridIndex);
        if (character) {
            float currentTransparency = character.image.GetComponent<Image>().color.a;
            Color newColor = ColorPalette.GetColor(ColorPalette.ColorEnum.White);
            newColor.a = currentTransparency;
            character.image.GetComponent<Image>().color = newColor;
            character.border.SetActive(false);
        }

        border.SetActive(false);
    }

    public bool IsHighlighed() {
        return isHighlighed;
    }
}