using UnityEngine;

public class ClickPicker : MonoBehaviour
{
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private LayerMask mask = ~0;

    [Header("FMOD")]
    [SerializeField] private FMODUnity.EventReference clickEvent;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit, maxDistance, mask)) return;

        // dŸwiêk klikniêcia

        if (hit.collider.TryGetComponent<LiquidSource>(out var source))
        {
            PlayClickSound(hit.point);
            source.Click();

        }
    }

    private void PlayClickSound(Vector3 hitPoint)
    {
        if (clickEvent.IsNull) return;

        // jeœli klik ma byæ "w œwiecie" (3D)
        FMODUnity.RuntimeManager.PlayOneShot(clickEvent, hitPoint);

        // FMODUnity.RuntimeManager.PlayOneShot(clickEvent);
    }
}