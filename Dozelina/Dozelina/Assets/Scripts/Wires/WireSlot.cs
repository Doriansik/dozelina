using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WireSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Color expectedColor;
    [SerializeField] private Image slotImage; // opcjonalnie, żeby też znikał / zmieniał kolor

    private bool done;

    public void SetExpectedColor(Color c)
    {
        expectedColor = c;
        if (slotImage != null) slotImage.color = c; // jeśli chcesz pokazać kolor
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (done) return;

        var dragged = eventData.pointerDrag;
        if (dragged == null) return;

        var wire = dragged.GetComponent<Wire>();
        if (wire == null) return;

        if (ColorsEqual(wire.WireColor, expectedColor))
        {
            done = true;

            wire.SnapTo(transform);
            wire.Hide();
            HideSlot();

            WiresGameManager.Instance.WireCompleted();
        }
        else
        {
        }
    }

    private void HideSlot()
    {
        if (slotImage != null) slotImage.enabled = false;
        var cg = GetComponent<CanvasGroup>();
        if (cg == null) cg = gameObject.AddComponent<CanvasGroup>();
        cg.blocksRaycasts = false;

        // jeśli ma zniknąć całkiem:
        gameObject.SetActive(false); // albo Destroy(gameObject)
    }

    private bool ColorsEqual(Color a, Color b)
    {
        // porównanie z tolerancją (ważne jeśli kolory mogą być “prawie” identyczne)
        const float eps = 0.01f;
        return Mathf.Abs(a.r - b.r) < eps &&
               Mathf.Abs(a.g - b.g) < eps &&
               Mathf.Abs(a.b - b.b) < eps;
    }
}