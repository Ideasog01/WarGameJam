using UnityEngine;
using TMPro;
using UnityEngine.UI;
using StarterAssets;

public class PlayerInterface : MonoBehaviour
{

    [Header("Main Display")]

    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private TextMeshProUGUI ammoText;

    [SerializeField]
    private GameObject mainDisplayObj;

    [Header("Objectives Display")]

    [SerializeField]
    private TextMeshProUGUI objectiveDescriptionText;

    [SerializeField]
    private TextMeshProUGUI objectiveProgressionText;

    [SerializeField]
    private Animator objectiveAnimator;

    [Header("Letter Display")]

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

    [Header("General Display")]

    [SerializeField]
    private GameObject[] damageScreenArray;

    [SerializeField]
    private Animator gameOverAnimator;

    [SerializeField]
    private Animator transitionAnimator;

    [SerializeField]
    private bool fadeIn;

    [Header("Pause Menu")]

    [SerializeField]
    private GameObject pauseMenuObj;

    private static GameObject interactButton;

    private GameObject letterMesh;

    private GameManager gameManager;

    private bool _objectiveActive;

    private void Awake()
    {
        interactButton = GameObject.Find("InteractButton");
        interactButton.SetActive(false);
        gameManager = this.GetComponent<GameManager>();
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

        GameManager.EnableCamera(false);
        FirstPersonController.isMoving = false;
        
        letterMesh = meshObj;
        letterMesh.SetActive(false);

        // Curser
        CurserBehaviour(true);
    }

    public void ExitLetter() //Via Inspector
    {
        // Curser
        CurserBehaviour(false);
        
        GameManager.EnableCamera(true);
        FirstPersonController.isMoving = true;
        letterCanvas.SetActive(false);
        letterMesh.SetActive(true);
        letterMesh = null;
        DisplayInteractButton(true);
        SoldierCharacter.disableCombatMechanics = false;

        if(GameManager.interactItem.IsObjectiveAndIsActive)
        {
            GameManager.objectiveManager.UpdateObjective(1, Objective.ObjectiveType.FindItem);
            GameManager.interactItem.IsObjectiveAndIsActive = false;
        }

        if(GameManager.interactItem.IsHouseItem)
        {
            SaveManager.IncreaseItemCollection();
        }

        // Transition to the scene
        gameManager.LoadSceneTransition();
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

    private void CurserBehaviour(bool state)
    {
        Cursor.visible = state;
        
        if(state == true)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void UpdateHealth(int health, int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    public void UpdateAmmo(int ammo, int maxAmmo)
    {
        ammoText.text = "Ammo: " + ammo.ToString() + "/" + maxAmmo.ToString();
    }

    public void DisplayObjectives()
    {
        if (mainDisplayObj.activeSelf)
        {
            if (!_objectiveActive)
            {
                objectiveAnimator.SetTrigger("in");
                UpdateObjectiveText();
                _objectiveActive = true;
            }
            else
            {
                objectiveAnimator.SetTrigger("out");
                _objectiveActive = false;
            }
        }
    }

    public void UpdateObjectiveText()
    {
        Objective objective = ObjectiveManager.currentObjective;

        if (objective.DisplayProgression)
        {
            objectiveProgressionText.gameObject.SetActive(true);
            objectiveProgressionText.text = objective.Progression + "/" + objective.MaxProgress;
        }
        else
        {
            objectiveProgressionText.gameObject.SetActive(false);
        }

        objectiveDescriptionText.text = objective.ObjectiveDescription;
    }

    public void ToggleMainHUD()
    {
        if(mainDisplayObj.activeSelf)
        {
            mainDisplayObj.SetActive(false);
        }
        else
        {
            mainDisplayObj.SetActive(true);
        }
    }

    public void DisplayPauseMenu(bool active)
    {
        pauseMenuObj.SetActive(active);
    }
}
