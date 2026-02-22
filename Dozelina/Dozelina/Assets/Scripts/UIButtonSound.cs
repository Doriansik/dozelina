using UnityEngine;
using UnityEngine.UI;

public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private FMODUnity.EventReference clickEvent;

    private void Awake()
    {
        if (button == null) button = GetComponent<Button>();
        button.onClick.AddListener(PlayClick);
    }

    private void PlayClick()
    {
        FMODUnity.RuntimeManager.PlayOneShot(clickEvent);
    }
}