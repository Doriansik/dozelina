using UnityEngine;

public class WiresGameManager : MonoBehaviour
{
    public static WiresGameManager Instance;

    [SerializeField] private int totalWires = 4; // ustaw w Inspectorze
    private int remaining;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        remaining = totalWires;
    }

    public void WireCompleted()
    {
        remaining--;

        if (remaining <= 0)
        {
            // wszystkie kable zniknê³y/pod³¹czone
            ManagerScene.Instance.LoadGameplayScene();
        }
    }
}