using UnityEngine;

public class LiquidFillController : MonoBehaviour
{
    [Header("Material")]
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private string fillProperty = "_Fill"; // ustaw Fill Reference w ShaderGraph na _Fill

    [Header("Fill")]
    [Range(0f, 1f)]
    [SerializeField] private float startFill = 1f;
    [SerializeField] private float drainSpeed = 0.5f; // ile fill/s na sekundê

    private Material _matInstance;
    private int _fillId;
    private float _currentFill;
    private float _targetFill;

    private void Awake()
    {
        if (!targetRenderer) targetRenderer = GetComponent<Renderer>();

        // UWAGA: .material tworzy instancjê (OK dla jednego obiektu z p³ynem)
        _matInstance = targetRenderer.material;
        _fillId = Shader.PropertyToID(fillProperty);

        _currentFill = Mathf.Clamp01(startFill);
        _targetFill = _currentFill;
        _matInstance.SetFloat(_fillId, _currentFill);
    }

    private void Update()
    {
        if (Mathf.Approximately(_currentFill, _targetFill)) return;

        _currentFill = Mathf.MoveTowards(_currentFill, _targetFill, drainSpeed * Time.deltaTime);
        _matInstance.SetFloat(_fillId, _currentFill);
    }

    // ZjedŸ do konkretnej wartoœci 0..1
    public void DrainTo(float targetFill01)
    {
        _targetFill = Mathf.Clamp01(targetFill01);
    }

    // ZjedŸ o "delta" (np. 0.2)
    public void DrainBy(float amount01)
    {
        DrainTo(_targetFill - Mathf.Abs(amount01));
    }

    public float CurrentFill => _currentFill;
}