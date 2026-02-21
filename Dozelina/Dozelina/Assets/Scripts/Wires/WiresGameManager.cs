using UnityEngine;

public class WiresGameManager : MonoBehaviour
{
    public static WiresGameManager Instance;

    [Header("Config")]
    [SerializeField] private int totalWires = 4;

    [Header("UI")]
    [SerializeField] private GameObject panelRoot;

    [Header("Refs")]
    [SerializeField] private OpenWeapon openWeapon;

    private int remaining;
    private bool active;

    private void Awake()
    {
        Instance = this;
        if (panelRoot) panelRoot.SetActive(false);
    }

    public void StartMinigame()
    {
        remaining = totalWires;
        active = true;

        if (panelRoot) panelRoot.SetActive(true);


        // Jeœli gdzieœ pauzujesz grê timeScale=0, zostaw — OpenWeapon i tak dzia³a na unscaled.
        // Jeœli nie chcesz pauzy, upewnij siê ¿e Time.timeScale = 1
        // Time.timeScale = 1f;
    }

    public void WireCompleted()
    {
        if (!active) return;

        remaining--;

        if (remaining <= 0)
        {
            panelRoot.SetActive(false);
            ManagerScene.Instance.LoadBuildScene();
        }
    }
}