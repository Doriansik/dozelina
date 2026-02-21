using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Wire : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Color wireColor;

    private Vector3 startPos;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    public Color WireColor => wireColor;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();

        startPos = rectTransform.position;
    }

    public void SetColor(Color color)
    {
        wireColor = color;
        GetComponent<Image>().color = color;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = rectTransform.position;
        transform.SetParent(canvas.transform, true);
        canvasGroup.blocksRaycasts = false; // wa¿ne, ¿eby slot dosta³ dropa
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // jeœli nie zosta³o przypiête do slota (czyli dalej siedzi na canvasie) -> wróæ
        if (transform.parent == canvas.transform)
            rectTransform.position = startPos;
    }

    public void SnapTo(Transform slot)
    {
        transform.SetParent(slot, false);
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public void Hide()
    {
        Destroy(gameObject); 
    }
}