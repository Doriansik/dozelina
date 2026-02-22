using UnityEngine;

public class DragDrop3D : MonoBehaviour
{
    public Camera cam;
    public float dragDistance = 2f;

    [Header("Drop detection")]
    public float slotSearchRadius = 1.5f; // jak daleko od czêœci szukaæ slota (œwiat)

    private DraggablePart3D dragging;

    private void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Pick();
        if (Input.GetMouseButton(0) && dragging != null) Drag();
        if (Input.GetMouseButtonUp(0) && dragging != null) Drop();
    }

    void Pick()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 500f))
        {
            var part = hit.collider.GetComponentInParent<DraggablePart3D>();
            if (part == null) return;
            if (part.IsLocked) return;

            dragging = part;
            Debug.Log("[DRAG] Picked: " + dragging.name);
        }
    }

    void Drag()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 target = ray.GetPoint(dragDistance);
        dragging.Drag(target);
    }

    void Drop()
    {
        Debug.Log("[DRAG] Drop: " + dragging.name);

        // Szukamy slotów KO£O czêœci (bez masek/warstw)
        Collider[] hits = Physics.OverlapSphere(dragging.transform.position, slotSearchRadius);
        Debug.Log("[DRAG] OverlapSphere hits: " + hits.Length);

        PartSlot3D best = null;
        float bestDist = float.MaxValue;

        foreach (var h in hits)
        {
            var slot = h.GetComponentInParent<PartSlot3D>();
            if (slot == null) continue;

            Transform sp = slot.snapPoint != null ? slot.snapPoint : slot.transform;
            float d = Vector3.Distance(dragging.transform.position, sp.position);

            Debug.Log($"[DRAG] Found slot via collider: {h.name} -> {slot.name}, dist={d:F3}");

            if (d < bestDist)
            {
                bestDist = d;
                best = slot;
            }
        }

        if (best != null)
        {
            bool placed = best.TryPlace(dragging);
            if (placed)
            {
                dragging = null;
                return;
            }
            Debug.Log("[DRAG] Slot found, but not placed (too far or occupied).");
        }
        else
        {
            Debug.Log("[DRAG] No slot found near the part.");
        }

        dragging.ReturnToStart();
        dragging = null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (dragging == null) return;
        Gizmos.DrawWireSphere(dragging.transform.position, slotSearchRadius);
    }
#endif
}