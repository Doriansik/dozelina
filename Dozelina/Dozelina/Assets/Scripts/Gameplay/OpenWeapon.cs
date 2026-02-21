using System.Collections;
using UnityEngine;

public class OpenWeapon : MonoBehaviour
{
    [SerializeField] private Transform window;
    [SerializeField] private Camera cam;

    [Header("Camera")]
    [SerializeField] private float zoomedFOV = 30f;
    [SerializeField] private float zoomDuration = 0.35f;

    [Header("Rotation")]
    [SerializeField] private float rotateDuration = 0.35f;
    [SerializeField] private float stepAngle = 90f;

    [Header("Minigame")]
    [SerializeField] private WiresGameManager wiresGame;
    [SerializeField] private float delayBeforeMinigame = 1.0f;

    private float defaultFOV;
    private bool isAnimating;

    private void Start()
    {
        if (!cam) cam = Camera.main;
        if (!cam) Debug.LogError("[OpenWeapon] Brak kamery (Camera.main). Sprawdü tag MainCamera.");

        defaultFOV = cam ? cam.fieldOfView : 60f;
        Debug.Log($"[OpenWeapon] Start | cam={(cam ? cam.name : "null")} defaultFOV={defaultFOV}");
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0) || isAnimating) return;

        if (!cam) cam = Camera.main;
        if (!cam) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;

        if (hit.collider.transform == window || hit.collider.transform.IsChildOf(window))
        {
            StartCoroutine(OpenAndStartMinigame());
        }
    }

    private IEnumerator OpenAndStartMinigame()
    {
        isAnimating = true;

        Quaternion startRot = window.rotation;
        Quaternion targetRot = startRot * Quaternion.Euler(stepAngle, 0f, 0f);

        float startFOV = cam.fieldOfView;

        float t = 0f;
        while (t < 1f)
        {
            // unscaled -> dzia≥a nawet jak timeScale=0
            t += Time.unscaledDeltaTime / rotateDuration;

            window.rotation = Quaternion.Slerp(startRot, targetRot, t);
            cam.fieldOfView = Mathf.Lerp(startFOV, zoomedFOV, t);

            yield return null;
        }

        window.rotation = targetRot;
        cam.fieldOfView = zoomedFOV;

        isAnimating = false;

        // delay teø unscaled:
        float wait = 0f;
        while (wait < delayBeforeMinigame)
        {
            wait += Time.unscaledDeltaTime;
            yield return null;
        }

        Debug.Log("[OpenWeapon] Starting minigame...");
        if (wiresGame) wiresGame.StartMinigame();
        else Debug.LogWarning("[OpenWeapon] wiresGame NOT assigned!");
    }

    public void ReturnCamera()
    {
        Debug.Log("[OpenWeapon] ReturnCamera() called");
        StopAllCoroutines();
        StartCoroutine(ReturnCameraRoutine());
    }

    private IEnumerator ReturnCameraRoutine()
    {
        if (!cam) cam = Camera.main;
        if (!cam)
        {
            Debug.LogError("[OpenWeapon] ReturnCameraRoutine: brak cam");
            yield break;
        }

        float startFOV = cam.fieldOfView;

        Debug.Log($"[OpenWeapon] Returning FOV {startFOV} -> {defaultFOV} (timeScale={Time.timeScale})");

        float t = 0f;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / zoomDuration;
            cam.fieldOfView = Mathf.Lerp(startFOV, defaultFOV, t);
            yield return null;
        }

        cam.fieldOfView = defaultFOV;
        Debug.Log("[OpenWeapon] Return done");
    }
}