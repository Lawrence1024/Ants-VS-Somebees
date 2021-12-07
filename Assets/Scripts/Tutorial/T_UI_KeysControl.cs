//FileName: T_UI_KeysControl.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: Controlls the 4 keys displayed on the screen. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class T_UI_KeysControl : MonoBehaviour
{
    //The keys are in the order of "WSAD" 
    public List<GameObject> UIKeys;
    private float oldTime;
    private float newTime;

    // Start is called before the first frame update
    /* Method Name: Start()
     * Summary: Initialize the time variable.
     * @param N/A
     * @return N/A
     * Special Effects: N/A
     */
    void Start()
    {
        oldTime = Time.time;
    }
    // Update is called once per frame
    /* Method Name: Update()
    * Summary: Time is updated every frame.
    * @param N/A
    * @return N/A
    * Special Effects: N/A
    */
    void Update()
    {
        newTime = Time.time;
    }
    /* Method Name: glow(string direction)
    * Summary: Given a specific direction, glow that key.
    * @param direction: Indicate which key should be glowed.
    * @return N/A
    * Special Effects: N/A
    */
    public void glow(string direction)
    {
        int index=-1;
        if (direction == "up")
        {
            index = 0;
        }
        else if (direction == "down")
        {
            index = 1;
        }
        else if (direction == "left")
        {
            index = 2;
        }
        else if (direction == "right")
        {
            index = 3;
        }
        else
        {
            Debug.Log("There is an error in T_UI_KeysControl glow function. Error: invalid input");
        }
        UIKeys[index].GetComponentInChildren<Image>().color = new Vector4(0.62f, 0.93f, 1f, 1f);
    }
    /* Method Name: dark(string direction)
    * Summary: Given a specific direction, darken that key.
    * @param direction: Indicate which key should be darkened.
    * @return N/A
    * Special Effects: N/A
    */
    public void dark(string direction)
    {
        int index = -1;
        if (direction == "up")
        {
            index = 0;
        }
        else if (direction == "down")
        {
            index = 1;
        }
        else if (direction == "left")
        {
            index = 2;
        }
        else if (direction == "right")
        {
            index = 3;
        }
        else
        {
            Debug.Log("There is an error in T_UI_KeysControl glow function. Error: invalid input");
        }
        UIKeys[index].GetComponentInChildren<Image>().color = new Vector4(1f, 1f, 1f, 1f);
    }
    /* Method Name: darkAllKeys()
    * Summary: Darken all keys on the screen.
    * @param N/A
    * @return N/A
    * Special Effects: N/A
    */
    public void darkAllKeys()
    {
        foreach(GameObject key in UIKeys)
        {
            key.GetComponentInChildren<Image>().color = new Vector4(1f, 1f, 1f, 1f);
        }
    }
}
