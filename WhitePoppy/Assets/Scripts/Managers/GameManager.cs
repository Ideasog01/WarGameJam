using Cinemachine;
using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static InteractItem.LevelToLoadByItem levelToLoadByItem;

    public static InteractItem interactItem;

    public static SoldierCharacter playerController;

    public static bool gameInProgress = true;

    private PlayerInterface playerInterface;

    

    [SerializeField]
    private bool combatScene = true;

    private void Awake()
    {
        if(combatScene)
        {
            playerController = GameObject.Find("Player").GetComponent<SoldierCharacter>();

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
        playerInterface.Transition(true);
        StartCoroutine("WaitBeforeDoingShitThankYou");
    }

    IEnumerator WaitBeforeDoingShitThankYou()
    {
        yield return new WaitForSeconds(2f);
        
        if (levelToLoadByItem == InteractItem.LevelToLoadByItem.None)
        {
            yield return null;
        }
        else if (levelToLoadByItem == InteractItem.LevelToLoadByItem.Level1)
        {
            SceneManager.LoadScene("Level 2");
        }
        else if (levelToLoadByItem == InteractItem.LevelToLoadByItem.Level2)
        {
            SceneManager.LoadScene("Level 3");
        }
        else if (levelToLoadByItem == InteractItem.LevelToLoadByItem.Level3)
        {
            SceneManager.LoadScene("Level 4");
        }
    }
}
