using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour
{
    private GridManager gridManager;
    private Button button;

    public void Setup(GridManager manager)
    {
        gridManager = manager;
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Vector2 pos = transform.position;
        gridManager.SelectCell(pos);
    }
}