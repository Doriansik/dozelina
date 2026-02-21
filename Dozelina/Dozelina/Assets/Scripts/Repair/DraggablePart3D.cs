using UnityEngine;

public class DraggablePart3D : MonoBehaviour
{
    public float followSpeed = 25f;

    private Vector3 startPos;
    private Quaternion startRot;
    private bool locked;

    private void Awake()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    public void Drag(Vector3 target)
    {
        if (locked) return;
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * followSpeed);
    }

    public void ReturnToStart()
    {
        if (locked) return;
        transform.position = startPos;
        transform.rotation = startRot;
    }

    public void LockTo(Transform snapPoint)
    {
        locked = true;
        transform.position = snapPoint.position;
        transform.rotation = snapPoint.rotation;

        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    public bool IsLocked => locked;
}