//FileName: T_PiecePosition.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: Controlls the modification of movement and position history for player and boxes (for tutorial). 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_PiecePosition : MonoBehaviour
{
    T_LevelManager levelManager;
    public GameObject player;
    public GameObject boxManager;
    private T_PlayerController playerScript;
    private T_BoxManager boxManagerScript;
    private List<GameObject> boxes;
    // Start is called before the first frame update
    /* Method Name: Start()
     * Summary: Initialize the value of different variables
     * @param N/A
     * @return N/A
     * Special Effects: N/A
     */
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<T_LevelManager>();
        playerScript = player.GetComponent<T_PlayerController>();
        boxManagerScript = boxManager.GetComponent<T_BoxManager>();
        boxes = boxManagerScript.allBoxes;
    }

    // Update is called once per frame
    void Update()
    {

    }
    /* Method Name: addBoxPos(string lastMove)
     * Summary: Add in an extra position to the position history of each box.
     * @param lastMove: The last move the user just performed.
     * @return N/A
     * Special Effects: N/A
     */
    public void addBoxPos(string lastMove)
    {
        foreach (GameObject box in boxes)
        {
            T_BoxController tempController = box.GetComponent<T_BoxController>();
            tempController.positionHistory.Add(new List<int> { tempController.xPos, tempController.yPos });
            tempController.movementHistory.Add("-");
        }
    }
    /* Method Name: addPlayerPos(string lastMove)
     * Summary: Add in an extra movement and position to the player's history.
     * @param lastMove: The last move the user just performed.
     * @return N/A
     * Special Effects: N/A
     */
    public void addPlayerPos(string lastMove)
    {
        playerScript.movementHistory.Add(lastMove);
        playerScript.positionHistory.Add(new List<int> { playerScript.xPos, playerScript.yPos });
    }
    /* Method Name: backBoxPos()
     * Summary: Remove the last position and movement from each box's history.
     * @param N/A
     * @return N/A
     * Special Effects: N/A
     */
    public void backBoxPos()
    {
        foreach (GameObject box in boxes)
        {
            T_BoxController tempController = box.GetComponent<T_BoxController>();
            tempController.positionHistory.RemoveAt(tempController.positionHistory.Count - 1);
            tempController.movementHistory.RemoveAt(tempController.movementHistory.Count - 1);
        }
    }
    /* Method Name: backPlayerPos()
     * Summary: Remove the last position and movement from player's history.
     * @param N/A
     * @return N/A
     * Special Effects: N/A
     */
    public void backPlayerPos()
    {
        playerScript.positionHistory.RemoveAt(playerScript.positionHistory.Count - 1);
        playerScript.movementHistory.RemoveAt(playerScript.movementHistory.Count - 1);
    }
    /* Method Name: whenHitBackButton()()
     * Summary: Reverse the player and the box's movement and position history. Undo the last move.
     * @param N/A
     * @return N/A
     * Special Effects: 20 points are deducted from the points in that level.
     */
    public void whenHitBackButton()
    {
        GameObject.Find("PointsValue").GetComponent<T_PointsCalculation>().points = GameObject.Find("PointsValue").GetComponent<T_PointsCalculation>().points - 20;
        if (GameObject.Find("PointsValue").GetComponent<T_PointsCalculation>().points <= 0)
        {
            GameObject.Find("PointsValue").GetComponent<T_PointsCalculation>().points = 0;
        }

        if (playerScript.movementHistory.Count != 0)
        {
            string lastSuccessMove = playerScript.movementHistory[playerScript.movementHistory.Count - 1].ToString();
            playerScript.reversePlayerMove(lastSuccessMove);
            foreach (GameObject box in boxes)
            {
                T_BoxController tempController = box.GetComponent<T_BoxController>();
                tempController.reverseBoxMove();
            }
            backBoxPos();
        }
        else
        {
            StartCoroutine(levelManager.loadWarning("You are at the first step!", 0.8f));
        }
    }
    /* Method Name: whenHitResetButton()
     * Summary: Clear the movement and position history of the player and boxes. Reset the position of the pieces.
     * @param N/A
     * @return N/A
     * Special Effects: 50 points are deducted from the points in that level.
     */
    public void whenHitResetButton()
    {
        GameObject.Find("PointsValue").GetComponent<T_PointsCalculation>().points = GameObject.Find("PointsValue").GetComponent<T_PointsCalculation>().points - 50;
        if (GameObject.Find("PointsValue").GetComponent<T_PointsCalculation>().points <= 0)
        {
            GameObject.Find("PointsValue").GetComponent<T_PointsCalculation>().points = 0;
        }

        if (playerScript.movementHistory.Count != 0)
        {
            playerScript.resetPlayer();
            foreach (GameObject box in boxes)
            {
                T_BoxController tempController = box.GetComponent<T_BoxController>();
                tempController.resetBox();
            }
        }
        else
        {
            StartCoroutine(levelManager.loadWarning("You are at the first step!", 0.8f));
        }
    }
}

