//FileName: T_BoxManager.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: T_BoxManager is a centralized control unit of all the boxes. It consist a list of all the boxes.
//              Really similar to the BoxController class, just a little modification based on the scenario of the tutorial.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class T_BoxManager : MonoBehaviour
{
    public List<GameObject> allBoxes;
    T_LevelManager levelManager;
    // Start is called before the first frame update
    /* Method Name: Start()
     * Summary: Initialize the value of levelManager.
     * @param N/A
     * @return N/A
     * Special Effects: N/A
     */
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<T_LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    /* Method Name: checkIfWin()
     * Summary: Check if the level is completed by examining the status of each box.
     * @param N/A
     * @return N/A
     * Special Effects: If the level is completed, notify the levelManager to move on.
     */
    public void checkIfWin()
    {
        bool win = true;
        foreach (GameObject box in allBoxes)
        {
            if (!box.GetComponent<T_BoxController>().answered || !box.GetComponent<T_BoxController>().checkIfEnterQuestion())
            {
                win = false;
            }
        }
        if (win)
        {
            levelManager.TFController.nextStep();
        }
    }
}
