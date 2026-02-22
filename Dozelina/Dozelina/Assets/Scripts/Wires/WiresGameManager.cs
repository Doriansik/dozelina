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

    [SerializeField] private Animator animator;

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
    }

    public void WireCompleted()
    {
        if (!active) return;

        remaining--;

        if (remaining <= 0)
        {
            panelRoot.SetActive(false);
            ManagerScene.Instance.LoadBuildScene();
            animator.SetBool("IsMoving", true);
        }
    }
}