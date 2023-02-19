using Cinemachine;
using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static InteractItem.LevelToLoadByItem levelToLoad;

    public static InteractItem interactItem;

    public static GameObject playerController;

    public static ObjectiveManager objectiveManager;

    public static SoundSystem soundSystem;

    public static bool gameInProgress = true;

    private PlayerInterface playerInterface;

    [SerializeField]
    private bool combatScene = true;

    private void Awake()
    {
        playerController = GameObject.Find("Player");
        soundSystem = GameObject.Find("GameManager").GetComponent<SoundSystem>();

        if (combatScene)
        {
           
            objectiveManager = this.GetComponent<ObjectiveManager>();

            if(playerController == null)
            {
                Debug.LogError("Error: The GameManager variable 'combatScene' is set to true, but there is no SoliderCharacter in the scene. \nOnly set this variable to true if the scene has a SoldierCharacter object");
            }
        }

        playerInterface = this.GetComponent<PlayerInterface>();
    }

    public static void EnableCamera(bool active)
    {
        playerController.GetComponent<FirstPersonController>().enabled = active;
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadSceneTransition()
    {
        if(levelToLoad != InteractItem.LevelToLoadByItem.None)
        {
            playerInterface.Transition(true);
            StartCoroutine(WaitBeforeSceneTransition());
        }
    }

    public void PauseGame()
    {
        if(gameInProgress)
        {
            playerInterface.DisplayPauseMenu(true);
            gameInProgress = false;
            Time.timeScale = 0.0f;
            EnableCamera(false);
            SoldierCharacter.disableCombatMechanics = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            playerInterface.DisplayPauseMenu(false);
            gameInProgress = true;
            Time.timeScale = 1.0f;
            EnableCamera(true);
            SoldierCharacter.disableCombatMechanics = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitBeforeSceneTransition()
    {
        yield return new WaitForSeconds(2f);
        PlayerPrefs.SetInt("level", (int)levelToLoad);
        SceneManager.LoadScene(6);
    }
}
