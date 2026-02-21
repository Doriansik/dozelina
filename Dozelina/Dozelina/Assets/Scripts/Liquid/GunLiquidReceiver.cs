using UnityEngine;

public class GunLiquidReceiver : MonoBehaviour
{
    [SerializeField] private Renderer gunRenderer;

    [Header("URP Lit")]
    [SerializeField] private string emissionColorProperty = "_EmissionColor";

    [Header("Intensity")]
    [SerializeField] private float intensity = 0f;
    [SerializeField] private float intensityStep = 0.5f;
    [SerializeField] private float intensityMax = 50f;

    private Material _mat;
    private int _emissionId;
    private Color _lastColor = Color.black;

    private void Awake()
    {
        if (!gunRenderer) gunRenderer = GetComponent<Renderer>();

        if (!gunRenderer)
        {
            Debug.LogError("[GunLiquidReceiver] Brak Renderer na obiekcie (pod³¹cz gunRenderer w Inspectorze).");
            return;
        }

        _mat = gunRenderer.material;
        _emissionId = Shader.PropertyToID(emissionColorProperty);

        _mat.EnableKeyword("_EMISSION");

        Debug.Log($"[GunLiquidReceiver] Start. Material={_mat.name}");
    }

    public void AddLiquid(Color clickedColor)
    {
        if (_mat == null) return;

        _lastColor = clickedColor;
        intensity = Mathf.Min(intensity + intensityStep, intensityMax);

        _mat.SetColor(_emissionId, _lastColor * intensity * 4.5f);

        Debug.Log($"[GunLiquidReceiver] AddLiquid: color={clickedColor} intensity={intensity}");
    }
}