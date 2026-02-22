using System.Collections;
using UnityEngine;
using FMOD.Studio;

public class SpeechOne : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private FMODUnity.EventReference speechEvent;

    private EventInstance speechInstance;

    private void Awake()
    {
        StartCoroutine(ShowSpeech());
        StartCoroutine(HideSpeech());
    }

    private IEnumerator ShowSpeech()
    {
        yield return new WaitForSeconds(3.5f);

        animator.SetTrigger("Enable");

        speechInstance = FMODUnity.RuntimeManager.CreateInstance(speechEvent);
        speechInstance.set3DAttributes(
            FMODUnity.RuntimeUtils.To3DAttributes(transform)
        );
        speechInstance.start();


        yield return new WaitForSeconds(3.8f);
        speechInstance.stop(STOP_MODE.ALLOWFADEOUT);
        speechInstance.release();
    }

    private IEnumerator HideSpeech()
    {
        yield return new WaitForSeconds(7.5f);
        animator.SetTrigger("Disable");
    }
}