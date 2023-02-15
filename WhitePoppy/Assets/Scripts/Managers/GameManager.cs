using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static CinemachineBrain cameraBrain;

    public static InteractItem interactItem;

    public static SoldierCharacter playerController;

    public static bool gameInProgress = true;

    [SerializeField]
    private bool combatScene = true;

    private void Awake()
    {
        cameraBrain = GameObject.Find("MainCamera").GetComponent<CinemachineBrain>();

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
        cameraBrain.enabled = active;
        playerController.GetComponent<FirstPersonController>().enabled = active;
    }
}
