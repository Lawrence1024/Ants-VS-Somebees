//FileName: SetVolume.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: SetVolume changes the volume size. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SetVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    /* Method Name: modifyVolume(float sliderValue)
     * Summary: Change the audio mixer's value according to the sliderValue.
     * @param sliderValue: The slider's value
     * @return N/A
     * Special Effects: Change the audio mixer's value according to the sliderValue.
     */
    public void modifyVolume(float sliderValue) {
        GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().setScrollBarVal(sliderValue);
        audioMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
}
