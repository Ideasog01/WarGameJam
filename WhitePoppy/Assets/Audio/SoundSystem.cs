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
    private AudioSource[] musicSource;


    [SerializeField]
    private AudioSource[] soundEffectsSource;


    [SerializeField]
    private AudioSource[] loopedAudioFX;
    // BG / Transformed

    [SerializeField]
    private AudioSource[] TriggerAudioFX;
    // munitions / objective / UI

    int SceneIndex;

    //[SerializeField]
    //AudioSettings AudioSettings;

    //[SerializeField]
    //private Slider volumeSlider;
    //[SerializeField]
    //private Text volumeText;
    //[SerializeField]
    //private float volume;
    //[SerializeField]
    //private float volumeMin;
    //[SerializeField]
    //private float volumeMax;

    private void Awake()
    {
        //// Set the volume of the game to the last saved volume
        //audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume", 0));

        SceneIndex = SceneManager.GetActiveScene().buildIndex;

        // get current scene index
        // if scene index = 0, play menu music
        // if scene index = 1, play level 1 music
        // if scene index = 2, play level 2 music
        // if scene index = 3, play level 3 music
        // if scene index = 4, play level 4 music

        // MAIN MENU 0
        // LEVEL 1 1
        // LEVEL 2 2
        // LEVEL 3 3
        // LEVEL 4 4

        PlayMusic(SceneIndex -1);

        if (SceneIndex == 4)
        {
            PlayLoopedSFX(0); // rain
            //PlayLoopedSFX(1); // wind

            StartCoroutine(DistantSFXTimer(2, 1, 8)); // transformed explosions
            StartCoroutine(VariableMunitonSFX(3, 3, 40)); // distant explosions
            StartCoroutine(DistantSFXTimer(4, 10, 80)); // thunder
            StartCoroutine(DistantSFXTimer(5, 15, 40)); // machinegun
            StartCoroutine(VariableMunitonSFX(6, 1, 10)); // bullet flyby
            StartCoroutine(DistantSFXTimer(7, 1, 3)); // potshots transformed
            StartCoroutine(VariableMunitonSFX(8, 1, 4)); // distant gunfire
        }
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
        musicSource[level].Play();
        musicSource[level].loop= true;
    }

    public void PlaySoundEffect(int soundEffect)
    {
        // Play the sound effect
        soundEffectsSource[soundEffect].Play();
    }


    void PlayMunitionSFX(int Munition)
    {
        loopedAudioFX[Munition].Play();
        loopedAudioFX[Munition].volume = Random.Range(0.5f, 1.0f);
        loopedAudioFX[Munition].panStereo = Random.Range(-1.0f, 1.0f);
        loopedAudioFX[Munition].pitch = Random.Range(0.5f, 1.5f);
    }

    public void PlayLoopedSFX(int loopedSound) 
    {
        loopedAudioFX[loopedSound].Play();
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