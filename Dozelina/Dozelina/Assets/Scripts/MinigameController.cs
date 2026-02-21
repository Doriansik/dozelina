using UnityEngine;

public class MinigameController : MonoBehaviour
{
    [SerializeField] private CustomerQueueManager queue;

    private GameObject _activeWeapon;

    public void StartMinigame(GameObject weaponInstance)
    {
        _activeWeapon = weaponInstance;
        Debug.Log("[Minigame] Start dla broni: " + (_activeWeapon ? _activeWeapon.name : "null"));

        // TODO: tu odpalasz swoj¹ minigrê (UI, interakcja, cokolwiek)
    }

    // Wo³asz to z logiki minigry
    public void FinishMinigame(bool success)
    {
        Debug.Log("[Minigame] Koniec. Sukces=" + success);

        if (success)
        {
            // opcjonalnie: "naprawiona" broñ, efekty, itd.

            // przejœcie do nastêpnego klienta
            queue.AdvanceQueueFromMinigame();
        }
        else
        {
            // jeœli pora¿ka ma coœ robiæ, to tu
            // np. odblokuj ponowne klikniêcie drop spotu itp.
        }
    }
}