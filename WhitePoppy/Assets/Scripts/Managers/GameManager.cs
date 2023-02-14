using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static CinemachineBrain cameraBrain;

    public static InteractItem interactItem;

    private void Awake()
    {
        cameraBrain = GameObject.Find("MainCamera").GetComponent<CinemachineBrain>();
    }

    public static void EnableCamera(bool active)
    {
        cameraBrain.enabled = active;
    }
}
