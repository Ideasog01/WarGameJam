using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ASArray : MonoBehaviour
{
    [SerializeField]
    public AudioSource[] array;

    [SerializeField]
    private AudioSource[] TriggerAudioFX;
    // munitions / objective / UI


    private void Awake()
    {
     
        PlayLoopedSFX(0); // rain
        //PlayLoopedSFX(1); // wind
        
        StartCoroutine(VariableMunitonSFX(2, 8, 77)); // transformed explosions
        StartCoroutine(VariableMunitonSFX(3, 3, 28)); // distant explosions
        StartCoroutine(DistantSFXTimer(4, 10, 52)); // thunder
        StartCoroutine(DistantSFXTimer(5, 15, 40)); // machinegun
        StartCoroutine(VariableMunitonSFX(6, 6, 10)); // bullet flyby
        StartCoroutine(DistantSFXTimer(7, 10, 30)); // potshots transformed
        StartCoroutine(DistantSFXTimer(8, 8, 35)); // distant gunfire
        StartCoroutine(DistantSFXTimer(9, 15, 30)); // distant gunfire
        StartCoroutine(DistantSFXTimer(10, 7, 30)); // distant gunfire
        StartCoroutine(DistantSFXTimer(11, 9, 20)); // distant gunfire
        StartCoroutine(DistantSFXTimer(12, 9, 26)); // distant gunfire

        StartCoroutine(VariableMunitonSFX(13, 6, 50)); // distant explosions

        StartCoroutine(VariableMunitonSFX(14, 7, 40)); // distant explosions

        StartCoroutine(VariableMunitonSFX(15, 4, 57)); // distant explosions

        StartCoroutine(VariableMunitonSFX(16, 7, 50)); // distant explosions





    }

    // Modular timer that plays the distant SFX at random intervals
    IEnumerator DistantSFXTimer(int SFX, int bot, int top)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(bot, top));
            PlayLoopedSFX(SFX);
        }
    }

    IEnumerator VariableMunitonSFX(int SFX, int bot, int top)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(bot, top));
            PlayMunitionSFX(SFX);
        }
    }

    /*
     different types of looped SFX
    1 = rain
    2 = wind
    3 = distantexplosions (sporadic)
    4 = distantexplosions2 (sporadic)
    4 = thunder (sporadic)
    5 = machinegun (random bursts)
    6 = bulletflyby (random sporadic)
    8 = potshots (random sporadic)
    9 = distant gunfire (random sporadic)
    */

    //public void SetVolume(float volume)
    //{
    //    // Set the volume of the game
    //    audioMixer.SetFloat("Volume", volume);
    //    PlayerPrefs.SetFloat("Volume", volume);
    //    PlayerPrefs.Save();
    //}

    public void PlayMusic(int level)
    {
        // Play the music for the level
        array[level].Play();
        array[level].loop = true;
    }

    public void PlaySoundEffect(int soundEffect)
    {
        // Play the sound effect
        array[soundEffect].Play();
    }


    void PlayMunitionSFX(int Munition)
    {
        array[Munition].Play();
        array[Munition].volume = Random.Range(0.5f, 1.0f);
        array[Munition].panStereo = Random.Range(-1.0f, 1.0f);
        array[Munition].pitch = Random.Range(0.5f, 1.5f);
    }

    public void PlayLoopedSFX(int loopedSound)
    {
        array[loopedSound].Play();
    }

    // Update is called once per frame
    void Update()
    {

        //// Update the volume slider and text
        //volume = PlayerPrefs.GetFloat("Volume", 0);
        //volumeSlider.value = volume;
        //volumeText.text = "Volume: " + volume.ToString("0.00");
    }
}