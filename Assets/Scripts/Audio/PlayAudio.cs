//FileName: PlayAudio.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: PlayAudio contains the functions to play the audio.  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class PlayAudio : MonoBehaviour
{
    public AudioClip[] clip;
    public AudioSource audioSource;
    public AudioClip movementSound;
    public AudioSource movementAudioSource;
    public AudioClip correctSound;
    public AudioSource correctAudioSource;
    public AudioClip wrongSound;
    public AudioSource wrongAudioSource;
    public AudioMixerGroup audioMixer;
    public AudioSource[] audArry;
    public float sliderValue;
    private GameObject volumePercentageText;
    private int randomNumber;
    private bool audioActive=true;
    /* Method Name: Start()
     * Summary: Initiate all the audio files and asign audioMixer, clip, sould, etc. to each. 
     * @param N/A
     * @return N/A
     * Special Effects: Audio sources created.
     */
    void Start()
    {
        randomNumber = Random.Range(0, clip.Length);
        audioSource = audArry[0];
        movementAudioSource = audArry[1];
        correctAudioSource = audArry[2];
        wrongAudioSource = audArry[3];
        audioSource.clip = clip[randomNumber];
        audioSource.playOnAwake = false;
        movementAudioSource.volume = .5f;
        movementAudioSource.clip = movementSound;
        movementAudioSource.playOnAwake = false;
        movementAudioSource.outputAudioMixerGroup = audioMixer;
        correctAudioSource.volume = 5;
        correctAudioSource.clip = correctSound;
        correctAudioSource.playOnAwake = false;
        correctAudioSource.outputAudioMixerGroup = audioMixer;
        wrongAudioSource.volume = .5f;
        wrongAudioSource.clip = wrongSound;
        wrongAudioSource.playOnAwake = false;
        correctAudioSource.outputAudioMixerGroup = audioMixer;
        sliderValue = 1;
    }
    /* Method Name: Update()
     * Summary: When the background music stops, it random generate a music to play.
     * @param N/A
     * @return N/A
     * Special Effects: Play random music.
     */
    void Update()
    {
        if (!audioSource.isPlaying&&audioActive)
        {
            randomNumber = Random.Range(0, clip.Length);
            audioSource.clip = clip[randomNumber];
            startPlayingAudio();
        }
    }
    /* Method Name: startPlayingAudio()
     * Summary: Play the backgroundMusic.
     * @param N/A
     * @return N/A
     * Special Effects: Play the backgroundMusic.
     */
    public void startPlayingAudio() {
        audioSource.Play();
    }
    /* Method Name: changeVolume(float vol)
     * Summary: Change the volume of the background music.
     * @param vol: The volume size.
     * @return N/A
     * Special Effects: Background music volume changed.
     */
    public void changeVolume(float vol) {
        audioSource.volume = vol;
    }
    /* Method Name: playMovementSound()
     * Summary: Play the movement sound.
     * @param N/A
     * @return N/A
     * Special Effects: Play the movement sound.
     */
    public void playMovementSound()
    {
        movementAudioSource.Play();
    }
    /* Method Name: playWrongSound()
     * Summary: Play the wrong sound.
     * @param N/A
     * @return N/A
     * Special Effects: Play the wrong sound.
     */
    public void playWrongSound() {
        wrongAudioSource.Play();
    }
    /* Method Name: playCorrectSound()
     * Summary: Play the corrent sound.
     * @param N/A
     * @return N/A
     * Special Effects: Play the corrent sound.
     */
    public void playCorrectSound() {
        correctAudioSource.Play();
    }
    /* Method Name: setScrollBarVal(val)
     * Summary: Set the volume scroll bar to the volume size.
     * @param val: The volume size.
     * @return N/A
     * Special Effects: Text display changes for the volume.
     */
    public void setScrollBarVal(float val) {
        sliderValue = val;
        volumePercentageText = GameObject.Find("AudioVolumeText");
        volumePercentageText.GetComponent<TMPro.TextMeshProUGUI>().text = "Volume: " + (int)((sliderValue * 100))+"%";
    }
}
