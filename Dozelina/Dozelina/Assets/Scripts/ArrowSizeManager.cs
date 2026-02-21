using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowSizeManager : MonoBehaviour
{

    private Vector3 startScale;

    private void Awake()
    {
        startScale = transform.localScale;
    }

    private void OnMouseEnter()
    {
        transform.localScale = new Vector3(.7f, .7f, .7f);
    }

    private void OnMouseExit()
    {
        transform.localScale = startScale;
    }

}
