using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

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
        if (other.gameObject.tag == "Player" && !other.gameObject.GetComponent<FirstPersonController>().isCarrying)
        {
            player = other.gameObject;
            player.gameObject.GetComponent<FirstPersonController>().isMoving = false;
            player.gameObject.GetComponent<FirstPersonController>().isCarrying = true;
            stateDrivenCamera = GameObject.FindObjectOfType<CinemachineStateDrivenCamera>().gameObject;
            animator = stateDrivenCamera.GetComponent<Animator>();
            StartCoroutine(LookDown());

        }
        else if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<FirstPersonController>().isCarrying)
        {
            player = other.gameObject;
            player.gameObject.GetComponent<FirstPersonController>().isMoving = false;
            player.gameObject.GetComponent<FirstPersonController>().isCarrying = false;
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
        player.gameObject.GetComponent<FirstPersonController>().isMoving = true;
        Destroy(this.gameObject);
    }

    IEnumerator LookDownDrop()
    {
        animator.Play("LookDownCamera");
        yield return new WaitForSeconds(3f);
        if (droppedObject != null)
            droppedObject.SetActive(true);
        animator.Play("MainCamera");
        player.gameObject.GetComponent<FirstPersonController>().isMoving = true;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
