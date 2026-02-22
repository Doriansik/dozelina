using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Image oldSprite;
    [SerializeField] private Sprite newSprite;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject creditsButton;
    [SerializeField] private GameObject quitButton;

    [SerializeField] private Animator animator;

    private float timeToQuit = 1f;


    public void OnPlayGame()
    {
        SceneManager.LoadScene("Build");
        animator.SetTrigger("Blink");

    }

    public void OnCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnGoBackCredits()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnQuitGame()
    {
        oldSprite.sprite = newSprite;
        playButton.SetActive(false);
        creditsButton.SetActive(false);
        quitButton.SetActive(false);
        StartCoroutine(QuitGame());
    }

    private IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(timeToQuit);
        Application.Quit();
    }
}
