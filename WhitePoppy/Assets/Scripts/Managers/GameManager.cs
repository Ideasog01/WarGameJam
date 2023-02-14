using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static CinemachineBrain cameraBrain;

    private void Awake()
    {
        cameraBrain = GameObject.Find("Main Camera").GetComponent<CinemachineBrain>();
    }

    public static void EnableCamera(bool active)
    {
        cameraBrain.enabled = active;
    }
}
