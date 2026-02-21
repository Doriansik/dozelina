using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    public static ManagerScene Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadWireScene()
    {
        SceneManager.LoadScene("Wires");
    }

    public void LoadGameplayScene()
    {
        SceneManager.LoadScene("Gameplay"); // nazwa sceny gameplay
    }

    public void LoadBuildScene()
    {
        SceneManager.LoadScene("Build1");
    }
    public void LoadBuild2Scene()
    {
        SceneManager.LoadScene("Build2");
    }
}
