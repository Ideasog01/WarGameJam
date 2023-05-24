using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool startUp;

    [SerializeField]
    private GameObject introCanvas;

    [SerializeField]
    private GameObject gameIntroCanvas;

    [SerializeField]
    private Animator creditsAnimator;

    private void Awake()
    {
        if(startUp)
        {
            introCanvas.SetActive(false);
            gameIntroCanvas.SetActive(false);
        }
        else
        {
            startUp = true;
        }
    }

    public void StartGame()
    {
        int levelToLoad = PlayerPrefs.GetInt("level");

        PlayerPrefs.SetInt("level", 1);
        GameManager.levelToLoad = InteractItem.LevelToLoadByItem.House;
        SceneManager.LoadScene(6);
    }

    public void ResetGameData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ActivateCredits()
    {
        creditsAnimator.SetTrigger("activate");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void StartGameButton()
    {
        int levelToLoad = PlayerPrefs.GetInt("level");

        if(levelToLoad > 0) //Then the game has been played before, so move to the next scene immediately
        {
            GameManager.levelToLoad = (InteractItem.LevelToLoadByItem)levelToLoad;
            SceneManager.LoadScene(6);
        }
        else
        {
            gameIntroCanvas.SetActive(true);
            this.GetComponent<TextSequencer>().Quote();
        }
    }
}
