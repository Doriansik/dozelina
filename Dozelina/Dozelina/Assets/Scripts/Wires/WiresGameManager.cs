using UnityEngine;

public class WiresGameManager : MonoBehaviour
{
    public static WiresGameManager Instance { get; private set; }

    [Header("Config")]
    [SerializeField] private int totalWires = 4;

    [Header("UI")]
    [SerializeField] private GameObject panelRoot; // WiresMinigamePanel

    [Header("Refs")]
    [SerializeField] private CustomerQueueManager queue;
    [SerializeField] private OpenWeapon openWeapon;

    private int remaining;
    private bool active;

    private void Awake()
    {
        Instance = this;

        if (panelRoot)
            panelRoot.SetActive(false);
    }

    public void StartMinigame()
    {
        remaining = totalWires;
        active = true;

        if (panelRoot)
            panelRoot.SetActive(true);

        Debug.Log($"[WiresGame] START | remaining={remaining}");
    }

    public void WireCompleted()
    {
        if (!active) return;

        remaining--;
        Debug.Log($"[WiresGame] WireCompleted | remaining={remaining}");

        if (remaining <= 0)
            FinishMinigame();
    }

    private void FinishMinigame()
    {
        active = false;

        if (panelRoot)
            panelRoot.SetActive(false);

        Debug.Log("[WiresGame] SUCCESS");

        // kamera wraca p³ynnie
        if (openWeapon)
            openWeapon.ReturnCamera();
        else
            Debug.LogWarning("[WiresGame] openWeapon NOT assigned!");

        // kolejny klient
        if (queue)
            queue.AdvanceQueueFromMinigame();
        else
            Debug.LogWarning("[WiresGame] queue NOT assigned!");
    }
}