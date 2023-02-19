using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameIntroCanvas;

    public void StartGame()
    {
        int levelToLoad = PlayerPrefs.GetInt("level");

        if(levelToLoad == 0)
        {
            SceneManager.LoadScene(1);
            PlayerPrefs.SetInt("level", 1);
        }
        else
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }


    public void QuitApplication()
    {
        Application.Quit();
    }
}
