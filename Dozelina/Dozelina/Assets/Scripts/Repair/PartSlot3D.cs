using UnityEngine;

public class PartSlot3D : MonoBehaviour
{
    public GameObject brokenPart;
    public Transform snapPoint;

    [Header("Snap")]
    public float snapRadius = 0.6f;

    private bool occupied;

    public bool TryPlace(DraggablePart3D part)
    {
        if (occupied) { Debug.Log("[SLOT] Occupied"); return false; }

        Transform sp = snapPoint != null ? snapPoint : transform;

        float dist = Vector3.Distance(part.transform.position, sp.position);
        Debug.Log($"[SLOT] Dist to snapPoint = {dist:F3} (need <= {snapRadius:F3})");

        if (dist > snapRadius) return false;

        if (brokenPart != null) Destroy(brokenPart);

        part.LockTo(sp);
        occupied = true;

        Debug.Log("[SLOT] Placed OK");
        return true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Transform sp = snapPoint != null ? snapPoint : transform;
        Gizmos.DrawWireSphere(sp.position, snapRadius);
    }
#endif
}