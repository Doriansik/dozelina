using System.Collections;
using UnityEngine;

public class SpeechOne : MonoBehaviour
{
    [SerializeField] private Animator animator;


    private void Awake()
    {
        StartCoroutine(ShowSpeech());
        StartCoroutine(HideSpeech());
    }

    private IEnumerator ShowSpeech()
    {
        yield return new WaitForSeconds(1.75f);
        animator.SetTrigger("Enable");
    }

    private IEnumerator HideSpeech()
    {
        yield return new WaitForSeconds(4f);
        animator.SetTrigger("Disable");
    }
}
