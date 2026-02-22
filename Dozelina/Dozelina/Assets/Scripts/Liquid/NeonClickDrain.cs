using UnityEngine;

public class NeonClickDrain : MonoBehaviour
{
    [SerializeField] private LiquidFillController liquid;
    
    [SerializeField] private float drainBy = 0.2f;

    private void OnMouseDown()
    {
        liquid.DrainBy(drainBy);
    }
}