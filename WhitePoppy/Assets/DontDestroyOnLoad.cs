using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    [SerializeField]
    private int[] sceneToBeDestroyedOn;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach(int index in sceneToBeDestroyedOn)
        {
            if (scene.buildIndex == index)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
