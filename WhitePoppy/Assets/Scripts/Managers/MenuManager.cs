using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameIntroCanvas;

    [SerializeField]
    private Animator creditsAnimator;

    public void StartGame()
    {
        int levelToLoad = PlayerPrefs.GetInt("level");

        PlayerPrefs.SetInt("level", 1);
        SceneManager.LoadScene(6);
    }

    public void ActivateCredits()
    {
        creditsAnimator.SetTrigger("activate");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
