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

    private static GameObject interactButton;

    private GameObject letterMesh;

    private void Awake()
    {
        interactButton = GameObject.Find("InteractButton");
        interactButton.SetActive(false);
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
    }

    public static void DisplayInteractButton(bool active)
    {
        interactButton.SetActive(active);
    }
}
