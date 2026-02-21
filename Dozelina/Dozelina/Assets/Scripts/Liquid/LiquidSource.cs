using UnityEngine;

public class LiquidSource : MonoBehaviour
{
    [SerializeField] private Renderer sourceRenderer;
    [SerializeField] private GunLiquidReceiver gun;

    [Header("Shader Graph Reference Names")]
    [SerializeField] private string fillProperty = "Fill";
    [SerializeField] private string liquidColorProperty = "LiquidColor";

    [SerializeField] private float amountPerClick = 0.10f;

    private Material _mat;
    private int _fillId;
    private int _colorId;

    private void Awake()
    {
        if (!sourceRenderer) sourceRenderer = GetComponent<Renderer>();
        _mat = sourceRenderer.material;

        _fillId = Shader.PropertyToID(fillProperty);
        _colorId = Shader.PropertyToID(liquidColorProperty);
    }

    public void Click()
    {
        if (!gun)
        {
            Debug.LogWarning($"[LiquidSource] Brak podpiêtego GUN na {name}");
            return;
        }

        if (!_mat.HasProperty(_fillId))
        {
            Debug.LogWarning($"[LiquidSource] Materia³ nie ma property Fill: {fillProperty}");
            return;
        }

        float fill = _mat.GetFloat(_fillId);
        if (fill <= 0.001f)
        {
            Debug.Log($"[LiquidSource] Fill=0 na {name}");
            return;
        }

        float taken = Mathf.Min(amountPerClick, fill);
        _mat.SetFloat(_fillId, fill - taken);

        Color c = _mat.HasProperty(_colorId) ? _mat.GetColor(_colorId) : Color.white;
        Debug.Log($"[LiquidSource] Klik: color={c} taken={taken} -> wysy³am do gun");

        gun.AddLiquid(c);
    }
}