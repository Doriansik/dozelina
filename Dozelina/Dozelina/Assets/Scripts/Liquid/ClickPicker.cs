using UnityEngine;

public class ClickPicker : MonoBehaviour
{
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private LayerMask mask = ~0;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit, maxDistance, mask)) return;

        if (hit.collider.TryGetComponent<LiquidSource>(out var source))
        {
            source.Click();
        }
    }
}