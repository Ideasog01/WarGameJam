using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI progressText;

    [SerializeField]
    private Slider progressBar;

    [SerializeField]
    private float sceneLoadAnimationMultiplier;

    private float _currentLoadingValue;

    private AsyncOperation _operation;

    private void Start()
    {
        LoadLevel();
        Debug.Log("LOADING BEGAN! Loading value: " + _currentLoadingValue);
    }

    private void Update()
    {
        if (_operation != null)
        {
            float targetValue = _operation.progress / 0.9f;
            _currentLoadingValue = Mathf.MoveTowards(_currentLoadingValue, targetValue, sceneLoadAnimationMultiplier * Time.deltaTime);

            float progress = _currentLoadingValue * 100;

            progressText.text = progress.ToString("F0") + "%";
            progressBar.value = progress;
        }
    }

    public void LoadLevel()
    {
        ObjectiveManager.objectiveIndex = 0;
        StartCoroutine(LoadASync());
    }

    IEnumerator LoadASync()
    {
        yield return new WaitForSeconds(1);

        _operation = SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("level"));
        _operation.allowSceneActivation = false;

        while (!_operation.isDone)
        {
            Debug.Log("LOADING PROGRESS: " + _operation.progress);

            if (_currentLoadingValue == 1f)
            {
                _operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}