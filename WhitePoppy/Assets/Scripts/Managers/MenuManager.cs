using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool startUp;

    [SerializeField]
    private GameObject gameIntroCanvas;

    [SerializeField]
    private Animator creditsAnimator;

    private void Awake()
    {
        if(!startUp)
        {
            startUp = true;
        }
        else
        {
            gameIntroCanvas.SetActive(false);
        }
    }

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
