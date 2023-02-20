using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSequence : MonoBehaviour
{
    [SerializeField]
    private Animator endAnimator;

    public void ActivateEndSequence()
    {
        endAnimator.SetTrigger("active");
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        GameManager.levelToLoad = InteractItem.LevelToLoadByItem.End;
        GameObject.Find("GameManager").GetComponent<GameManager>().LoadSceneTransition();
    }
}
