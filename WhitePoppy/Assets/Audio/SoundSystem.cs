using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundSystem : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private Transform audioSourcePrefab;

    [SerializeField]
    private Sound[] environmentSounds;

    private List<AudioSource> _audioSourceList = new List<AudioSource>();

    int SceneIndex;

    private void Awake()
    {
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (SceneIndex == 4)
        {
             // rain
            //PlayLoopedSFX(1); // wind

            

            //StartCoroutine(DistantSFXTimer(2, 1, 8)); // transformed explosions
            //StartCoroutine(VariableMunitonSFX(3, 3, 40)); // distant explosions
            //StartCoroutine(DistantSFXTimer(4, 10, 80)); // thunder
            //StartCoroutine(DistantSFXTimer(5, 15, 40)); // machinegun
            //StartCoroutine(VariableMunitonSFX(6, 1, 10)); // bullet flyby
            //StartCoroutine(DistantSFXTimer(7, 1, 3)); // potshots transformed
            //StartCoroutine(VariableMunitonSFX(8, 1, 4)); // distant gunfire
        }

        foreach(Sound sound in environmentSounds)
        {
            StartCoroutine(PlayEnvironmentSound(sound));
        }
    }

    // Modular timer that plays the distant SFX at random intervals
    IEnumerator PlayEnvironmentSound(Sound sound)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(sound.MinWaitTime, sound.MaxWaitTime));
            PlaySound(sound);
        }
    }

    public void PlaySound(Sound sound)
    {
        AudioSource audio = null;

        foreach(AudioSource audioItem in _audioSourceList)
        {
            if(!audioItem.isPlaying)
            {
                audio = audioItem;
                audio.transform.position = sound.AttachPoint.position;
                break;
            }
        }

        if(audio == null)
        {
            audio = Instantiate(audioSourcePrefab.GetComponent<AudioSource>(), sound.AttachPoint.position, Quaternion.identity, this.transform);
        }

        audio.loop = sound.LoopSound;
        audio.volume = sound.Volume;
        audio.pitch = sound.Pitch;
        audio.panStereo = sound.PanStereo;
        audio.clip = sound.Clip;

        audio.Play();
    }
}

[System.Serializable]
public struct Sound
{
    [SerializeField]
    private bool loopSound;

    [SerializeField]
    private float volume;

    [SerializeField]
    private float panStereo;

    [SerializeField]
    private float pitch;

    [SerializeField]
    private AudioClip audioClip;

    [SerializeField]
    private Transform attachPoint;

    [SerializeField]
    private int minWaitTime;

    [SerializeField]
    private int maxWaitTime;

    public bool LoopSound
    {
        get { return loopSound; }
    }

    public float Volume
    {
        get { return volume; }
    }

    public float PanStereo
    {
        get { return panStereo; }
    }

    public float Pitch
    {
        get { return pitch; }
    }

    public AudioClip Clip
    {
        get { return audioClip; }
    }

    public Transform AttachPoint
    {
        get { return attachPoint; }
    }

    public int MaxWaitTime
    {
        get { return maxWaitTime; }
    }

    public int MinWaitTime
    {
        get { return minWaitTime; }
    }
}