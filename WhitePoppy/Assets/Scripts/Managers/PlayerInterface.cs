using UnityEngine;
using TMPro;
using UnityEngine.UI;
using StarterAssets;

public class PlayerInterface : MonoBehaviour
{
    [SerializeField]
    private GameObject letterCanvas;

    [SerializeField]
    private TextMeshProUGUI letterDate;

    [SerializeField]
    private TextMeshProUGUI letterAddressee;

    [SerializeField]
    private TextMeshProUGUI letterContent;

    [SerializeField]
    private TextMeshProUGUI letterSender;

    [SerializeField]
    private GameObject[] damageScreenArray;

    [SerializeField]
    private Animator gameOverAnimator;

    [SerializeField]
    private Animator transitionAnimator;

    [SerializeField]
    private bool fadeIn;

    private static GameObject interactButton;

    private GameObject letterMesh;

    private void Awake()
    {
        interactButton = GameObject.Find("InteractButton");
        interactButton.SetActive(false);
    }

    private void Start()
    {
        if(fadeIn)
        {
            Transition(false);
        }
    }

    public void DisplayLetter(string date, string addressee, string content, string sender, GameObject meshObj)
    {
        letterCanvas.SetActive(true);
        letterDate.text = date;
        letterAddressee.text = addressee;
        letterContent.text = content;
        letterSender.text = sender;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameManager.EnableCamera(false);
        FirstPersonController.isMoving = false;
        letterMesh = meshObj;
        letterMesh.SetActive(false);
    }

    public void ExitLetter() //Via Inspector
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.EnableCamera(true);
        FirstPersonController.isMoving = true;
        letterCanvas.SetActive(false);
        letterMesh.SetActive(true);
        letterMesh = null;
        DisplayInteractButton(true);
        SoldierCharacter.disableCombatMechanics = false;
    }

    public static void DisplayInteractButton(bool active)
    {
        interactButton.SetActive(active);
    }

    public void UpdateDamageScreen(int health)
    {
        if(health > 60 && health < 80)
        {
            damageScreenArray[0].SetActive(true);
            damageScreenArray[1].SetActive(false);
            damageScreenArray[2].SetActive(false);
        }
        
        if(health <= 60 && health > 30)
        {
            damageScreenArray[0].SetActive(true);
            damageScreenArray[1].SetActive(true);
            damageScreenArray[2].SetActive(false);
        }
        
        if(health <= 30)
        {
            damageScreenArray[0].SetActive(true);
            damageScreenArray[1].SetActive(true);
            damageScreenArray[2].SetActive(true);
        }
    }

    public void DisplayGameOverScreen()
    {
        gameOverAnimator.gameObject.SetActive(true);
        gameOverAnimator.SetTrigger("trigger");
        GameManager.gameInProgress = false;
    }

    public void Transition(bool display)
    {
        if(display)
        {
            transitionAnimator.SetTrigger("fadeIn");
        }
        else
        {
            transitionAnimator.SetTrigger("fadeOut");
        }
    }
}
