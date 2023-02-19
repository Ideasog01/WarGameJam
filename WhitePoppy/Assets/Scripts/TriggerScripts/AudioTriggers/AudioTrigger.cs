using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{

    public AudioSource source;
    //public AudioClip clip;

    [SerializeField]
    public bool disableonend = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //source.PlayOneShot(clip);
            source.Play();
        }
        if (disableonend)
        {
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
