using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraLookDown : MonoBehaviour
{
    #region Variables
    private GameObject stateDrivenCamera;
    private Animator animator;

    [SerializeField]
    private GameObject droppedObject;

    private GameObject player;
    #endregion

    //Ignore the repeatitive code :D 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !other.gameObject.GetComponent<BaseCharacter>().isCarrying)
        {
            player = other.gameObject;
            player.gameObject.GetComponent<BaseCharacter>().isMoving = false;
            player.gameObject.GetComponent<BaseCharacter>().isCarrying = true;
            stateDrivenCamera = GameObject.FindObjectOfType<CinemachineStateDrivenCamera>().gameObject;
            animator = stateDrivenCamera.GetComponent<Animator>();
            StartCoroutine(LookDown());

        }
        else if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<BaseCharacter>().isCarrying)
        {
            player = other.gameObject;
            player.gameObject.GetComponent<BaseCharacter>().isMoving = false;
            player.gameObject.GetComponent<BaseCharacter>().isCarrying = false;
            stateDrivenCamera = GameObject.FindObjectOfType<CinemachineStateDrivenCamera>().gameObject;
            animator = stateDrivenCamera.GetComponent<Animator>();
            StartCoroutine(LookDownDrop());
        }
    }

    IEnumerator LookDown()
    {
        animator.Play("LookDownCamera");
        yield return new WaitForSeconds(3f);
        droppedObject.SetActive(false);
        animator.Play("MainCamera");
        player.gameObject.GetComponent<BaseCharacter>().isMoving = true;
        Destroy(this.gameObject);
    }

    IEnumerator LookDownDrop()
    {
        animator.Play("LookDownCamera");
        yield return new WaitForSeconds(3f);
        if (droppedObject != null)
            droppedObject.SetActive(true);
        animator.Play("MainCamera");
        player.gameObject.GetComponent<BaseCharacter>().isMoving = true;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
