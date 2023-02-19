using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextSequencer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI descriptionText;

    [TextArea(5, 10)]
    [SerializeField]
    private string[] textArray;

    [SerializeField]
    private Animator textAnimator;

    private bool _isActivated;

    private int _index;

    public void Quote()
    {
        textAnimator.SetTrigger("quote");
    }

    public void NextObj()
    {
        if(_isActivated)
        {
            if(_index + 1 == textArray.Length)
            {
                textAnimator.SetTrigger("prefaceOutro");
            }
            else
            {
                StartCoroutine(DelayDisplay());
            }
        }
        else
        {
            textAnimator.SetTrigger("prefaceIntro");
            _isActivated = true;
        }
    }

    private IEnumerator DelayDisplay()
    {
        textAnimator.SetTrigger("prefaceNext");

        yield return new WaitForSeconds(1f);

        _index++;
        descriptionText.text = textArray[_index];
    }
}
