using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector2 cardStartPosition;
    private Card card;

    void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        card = gameObject.GetComponent<Card>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.alpha = 0.6f;  // Make it slightly transparent
        canvasGroup.blocksRaycasts = false; // Allow raycast to pass through

        card.OnClick();
        cardStartPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta;  // Move with mouse
    }

    public async void OnEndDrag(PointerEventData eventData) {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true; // Allow raycast to pass through

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results) {
            GridCell gridCell = result.gameObject.GetComponent<GridCell>();
            if (gridCell) {
                await gridCell.OnClick();
                return;
            }
        }
        rectTransform.anchoredPosition = cardStartPosition;
        card.OnClick();
    }
}