using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject itemObj;

    [SerializeField]
    private GameObject itemDisplayCanvas;

    [SerializeField]
    private MeshFilter meshFilter;

    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private TextMeshProUGUI itemNameText;

    [SerializeField]
    private TextMeshProUGUI itemDescriptionText;

    [SerializeField]
    private GameObject crossHair;

    [SerializeField]
    private float itemRotationSpeed = 5;

    private GameObject _itemMesh;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = this.GetComponent<GameManager>();
    }

    public void DisplayItem(Mesh itemMesh, Material[] material, Vector3 scale, Vector3 rotation, string itemDescription, string itemName, GameObject itemMeshObj)
    {
        itemObj.SetActive(true);
        itemDisplayCanvas.SetActive(true);

        itemObj.transform.GetChild(0).localScale = scale;
        itemObj.transform.GetChild(0).rotation = Quaternion.Euler(rotation);

        meshFilter.mesh = itemMesh;
        meshRenderer.materials = material;

        itemDescriptionText.text = itemDescription;
        itemNameText.text = itemName;

        FirstPersonController.isMoving = false;
        GameManager.EnableCamera(false);

        _itemMesh = itemMeshObj;
        _itemMesh.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if(crossHair != null)
        {
            crossHair.SetActive(false);
        }
    }

    public void ExitItem()
    {
        itemObj.SetActive(false);
        itemDisplayCanvas.SetActive(false);

        FirstPersonController.isMoving = true;
        GameManager.EnableCamera(true);

        if(!GameManager.interactItem.IsObjectiveAndIsActive && SceneManager.GetActiveScene().buildIndex != 4)
        {
            _itemMesh.SetActive(true);
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        PlayerInterface.DisplayInteractButton(true);
        SoldierCharacter.disableCombatMechanics = false;

        if(crossHair != null)
        {
            crossHair.SetActive(true);
        }
        

        if (GameManager.interactItem.IsObjectiveAndIsActive)
        {
            GameManager.objectiveManager.UpdateObjective(1, Objective.ObjectiveType.FindItem);
            GameManager.interactItem.IsObjectiveAndIsActive = false;
        }

        if (GameManager.interactItem.IsHouseItem)
        {
            SaveManager.IncreaseItemCollection();
        }

        // Transition to the scene
        gameManager.LoadSceneTransition();
    }

    private void Update()
    {
        if(itemObj.activeSelf)
        {
            itemObj.transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * itemRotationSpeed);
        }
    }
}
