using System.Collections;
using UnityEngine;

public class OpenWeapon : MonoBehaviour
{
    [SerializeField] private Transform window;
    [SerializeField] private Camera cam;
    [SerializeField] private float zoomedFOV = 30f;
    [SerializeField] private float zoomDuration = 0.35f;

    [SerializeField] private LayerMask weaponLayer;

    [SerializeField] private float rotateDuration = 0.35f;
    [SerializeField] private float stepAngle = 90f;


    private bool isRotating;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0) || isRotating) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;

        if (hit.collider.gameObject == window || hit.collider.transform.IsChildOf(window.transform))
        {
            StartCoroutine(RotateBy(stepAngle));
        }
    }

    private IEnumerator RotateBy(float deltaYaw)
    {
        isRotating = true;

        Quaternion startRot = window.rotation;
        Quaternion targetRot = startRot * Quaternion.Euler(deltaYaw, 0f, 0f);

        float startFOV = cam.fieldOfView;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / rotateDuration;

            window.rotation = Quaternion.Slerp(startRot, targetRot, t);
            cam.fieldOfView = Mathf.Lerp(startFOV, zoomedFOV, t);

            yield return null;
        }

        window.rotation = targetRot;
        cam.fieldOfView = zoomedFOV;

        yield return new WaitForSeconds(1.5f);
        ManagerScene.Instance.LoadWireScene();
    }
}