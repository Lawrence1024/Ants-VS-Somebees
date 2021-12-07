//FileName: BoxManager.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: BoxManager is a centralized control unit of all the boxes. It consist a list of all the boxes.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class BeeManager : MonoBehaviour
{
    public List<GameObject> allBees;
    //LevelManager levelManager;
    // Start is called before the first frame update
    /* Method Name: Start()
     * Summary: Initialize the value of levelManager.
     * @param N/A
     * @return N/A
     * Special Effects: N/A
     */
    void Start()
    {
        //levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
//    /* Method Name: checkIfWin()
//     * Summary: Check if the level is completed by examining the status of each box.
//     * @param N/A
//     * @return N/A
//     * Special Effects: If the level is completed, notify the levelManager to move on.
//     */
//    public void checkIfWin()
//    {
//        bool win = true;
//        foreach(GameObject box in allBoxes)
//        {
//            if (!box.GetComponent<BoxController>().answered||!box.GetComponent<BoxController>().checkIfEnterQuestion())
//            {
//                win = false;
//            }
//        }
//        if (win)
//        {            
//            StartCoroutine(levelManager.buffer());
//        }
//    }
}
