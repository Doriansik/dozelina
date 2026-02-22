using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;

    [SerializeField] private ParticleSystem doneEffectVFX;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnDoneEffect(Vector3 position, Quaternion rotation)
    {
        var fx = Instantiate(doneEffectVFX, position, rotation);
        fx.Play();
        Destroy(fx.gameObject, 3f);
    }
}