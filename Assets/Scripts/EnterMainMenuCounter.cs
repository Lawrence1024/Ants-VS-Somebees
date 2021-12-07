//FileName: EnterMainMenuCounter.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: EnterMainMenuCounter is a place holder to see if the anwer button is correct or incorrect. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMainMenuCounter : MonoBehaviour
{
    public int mainMenuCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    /* Method Name: IncreaseMainMenuCounter()
     * Summary: Increase mainMenuCounter by 1.
     * @param N/A
     * @return N/A
     * Special Effects: ncrease mainMenuCounter by 1.
     */
    public void IncreaseMainMenuCounter() {
        mainMenuCounter ++;
    }
}
