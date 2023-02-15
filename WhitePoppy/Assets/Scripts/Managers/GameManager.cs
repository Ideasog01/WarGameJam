using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static InteractItem interactItem;

    public static SoldierCharacter playerController;

    public static bool gameInProgress = true;

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
}
