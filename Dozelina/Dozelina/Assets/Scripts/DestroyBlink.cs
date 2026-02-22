using UnityEngine;

public class DestroyBlink : MonoBehaviour
{
    private void Awake()
    {
        Die();
    }

    private void Die()
    {
        Destroy(gameObject,2f);
    }
}
