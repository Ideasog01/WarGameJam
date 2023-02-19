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
            PlayerPrefs.SetInt("level", 1);
            SceneManager.LoadScene(6);
        }
        else
        {
            SceneManager.LoadScene(6);
        }
    }


    public void QuitApplication()
    {
        Application.Quit();
    }
}
