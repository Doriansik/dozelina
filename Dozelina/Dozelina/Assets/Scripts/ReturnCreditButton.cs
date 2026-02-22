using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnCreditButton : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference creditsSound;

    public void OnReturnCredits()
    {
        FMODUnity.RuntimeManager.PlayOneShot(creditsSound);
        SceneManager.LoadScene("Menu");
    }
}
