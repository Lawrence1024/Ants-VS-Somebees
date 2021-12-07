//FileName: T_PlayerController.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: Controlls the player in the tutorial. Keep track of all player information. 
//              Really similar to the BoxController class, just a little modification based on the scenario of the tutorial.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    public LayerMask boxLayer;
    public ArrayList movementHistory = new ArrayList();
    public int xPos;
    public int yPos;
    public List<int> startingPosition;
    public Vector3 startingVectPosition;
    public List<List<int>> positionHistory = new List<List<int>>();
    public GameObject gameCanvas;
    private T_PiecePosition piecePosition;
    public string attemptMovement;
    public bool canMove = true;
    private float newTime;
    private float oldTime = 0f;
    public T_TutorialFlowController TFlowController;
    public int minorStepCounter = 0;
    public Account activeAccount;
    public float convertingScale;
    // Start is called before the first frame update
    /* Method Name: Start()
     * Summary: Initialize the different variables.
     * @param N/A
     * @return N/A
     * Special Effects: N/A
     */
    void Start()
    {
        movePoint.position = transform.position;
        positionHistory.Add(new List<int> { xPos, yPos });
        startingPosition = new List<int> { xPos, yPos };
        startingVectPosition = transform.position;
        piecePosition = gameCanvas.GetComponent<T_PiecePosition>();
        activeAccount = GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount;
        convertingScale = findConvertingScale();
    }

    // Update is called once per frame
    /* Method Name: Update()
    * Summary: The player will move towards the move point every frame. If certain conditions are met, the move point can move once more.
    *          As a result, the player will be able to move on the board.
    * @param N/A
    * @return N/A
    * Special Effects: Everytime the move point is moved, the method calls on other methods to recored the position and movement history.
    */
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        newTime = Time.time;
        bool state1 = Vector3.Distance(transform.position, movePoint.position) <= 0.05f;
        bool state2 = (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f);
        bool state3 = canMove;
        bool state4 = newTime - oldTime > 0.35f;
        bool state5 = TFlowController.currentStep > 55;
        if (state1 && state2 && state3 && state4 && !state5)
        {
            doTFlowController();
        }
        if (state1 && state2 && state3 && state4 && state5)
        {
            makeMovement();
            piecePosition.addPlayerPos(attemptMovement);
            piecePosition.addBoxPos(attemptMovement);
        }
    }
    /* Method Name: findConvertingScale()
     * Summary: Find the converting scale between 1 unit of global position and 1 unit of local position.
     * @param N/A
     * @return N/A
     * Special Effects: Used to calibrate game and accomidate different screen resolutions.
     */
    public float findConvertingScale()
    {
        float initialX = movePoint.transform.position.x;
        movePoint.localPosition += new Vector3(107.8949f, 0f, 0f);
        float finalX = movePoint.transform.position.x;
        movePoint.localPosition += new Vector3(-107.8949f, 0f, 0f);
        return finalX - initialX;
    }
    /* Method Name: doTFlowController()
     * Summary: Move the player according to the step count on the TutorialFlowController
     * @param N/A
     * @return N/A
     * Special Effects: Unwanted manuvers are blocked.
     */
    private void doTFlowController()
    {
        int step = TFlowController.currentStep;
        if(step>=0 && step <= 17)
        {
            streakMovement0To17();
        }else if (step >= 21 && step <= 46)
        {
            streakMovement21To31();
        }
        
    }
    /* Method Name: streakMovement0To17()
     * Summary: Check which movements to take when the step count is from 0 to 17.
     * @param N/A
     * @return N/A
     * Special Effects: Unwanted manuvers are blocked.
     */
    void streakMovement0To17()
    {
        bool up = Input.GetAxisRaw("Vertical") == 1f;
        bool down = Input.GetAxisRaw("Vertical") == -1f;
        bool left = Input.GetAxisRaw("Horizontal") == -1f;
        bool right = Input.GetAxisRaw("Horizontal") == 1f;
        if (TFlowController.currentStep == 0 && up)
        {
            moveFlow("up", 1);
        }
        else if (TFlowController.currentStep == 1 && down)
        {
            moveFlow("down", 1);
        }
        else if (TFlowController.currentStep == 2 && left)
        {
            moveFlow("left", 1);
        }
        else if (TFlowController.currentStep == 3 && right)
        {
            moveFlow("right", 1);
        }
        else if (TFlowController.currentStep == 4 && right)
        {
            moveFlow("right", 2);
        }
        else if (TFlowController.currentStep == 5 && down)
        {
            moveFlow("down", 2);
        }
        else if (TFlowController.currentStep == 6 && right)
        {
            moveFlow("right", 2);
        }
        else if (TFlowController.currentStep == 7 && up)
        {
            moveFlow("up", 3);
        }
        else if (TFlowController.currentStep == 8 && left)
        {
            moveFlow("left", 1);
        }
        else if (TFlowController.currentStep == 9 && left)
        {
            moveFlow("left", 1);
        }
        else if (TFlowController.currentStep == 10 && left)
        {
            moveFlow("left", 1);
        }
        else if (TFlowController.currentStep == 11 && down)
        {
            moveFlow("down", 1);
        }
        else if (TFlowController.currentStep == 12 && left)
        {
            moveFlow("left", 1);
        }
        else if (TFlowController.currentStep == 13 && up)
        {
            moveFlow("up", 2);
        }
        else if (TFlowController.currentStep == 14 && up)
        {
            moveFlow("up", 1);
        }
        else if (TFlowController.currentStep == 15 && right)
        {
            moveFlow("right", 1);
        }
        else if (TFlowController.currentStep == 16 && up)
        {
            moveFlow("up", 1);
        }
        else if (TFlowController.currentStep == 17 && left)
        {
            moveFlow("left", 2);
        }
    }
    /* Method Name: streakMovement21To31()
     * Summary: Check which movements to take when the step count is from 21 to 43.
     * @param N/A
     * @return N/A
     * Special Effects: Unwanted manuvers are blocked.
     */
    void streakMovement21To31()
    {
        bool up = Input.GetAxisRaw("Vertical") == 1f;
        bool down = Input.GetAxisRaw("Vertical") == -1f;
        bool left = Input.GetAxisRaw("Horizontal") == -1f;
        bool right = Input.GetAxisRaw("Horizontal") == 1f;
        if (TFlowController.currentStep == 21 && down)
        {
            moveFlow("down", 4);
        }
        else if (TFlowController.currentStep == 26 && left)
        {
            moveFlow("left", 2);
        }
        else if (TFlowController.currentStep == 27 && up)
        {
            moveFlow("up", 1);
        }
        else if (TFlowController.currentStep == 29 && up)
        {
            moveFlow("up", 2);
        }
        else if (TFlowController.currentStep == 30 && right)
        {
            moveFlow("right", 1);
        }
        else if (TFlowController.currentStep == 31 && down)
        {
            moveFlow("down", 1);
        }
        else if (TFlowController.currentStep == 32 && up)
        {
            moveFlow("up", 1);
        }
        else if (TFlowController.currentStep == 33 && right)
        {
            moveFlow("right", 3);
        }
        else if (TFlowController.currentStep == 34 && down)
        {
            moveFlow("down", 1);
        }
        else if (TFlowController.currentStep == 35 && left)
        {
            moveFlow("left", 3);
        }
        else if (TFlowController.currentStep == 36 && up)
        {
            moveFlow("up", 1);
        }
        else if (TFlowController.currentStep == 37 && left)
        {
            moveFlow("left", 1);
        }
        else if (TFlowController.currentStep == 38 && down)
        {
            moveFlow("down", 1);
        }
        else if (TFlowController.currentStep == 40 && down)
        {
            moveFlow("down", 2);
        }
        else if (TFlowController.currentStep == 41 && right)
        {
            moveFlow("right", 2);
        }
        else if (TFlowController.currentStep == 42 && up)
        {
            moveFlow("up", 1);
        }
        else if (TFlowController.currentStep == 43 && left)
        {
            moveFlow("left", 1);
        }

    }
    /* Method Name: moveFlow(string direct, int times)
     * Summary: A function to acutally check the conditoin and move the player a specific amount of steps torards a direction.
     * @param direct: the direction the player should move in.
     * @param times: The amount of steps the user should be moving in that direction.
     * @return N/A
     * Special Effects: Audio is played on each movement.
     */
    void moveFlow(string direct, int times)
    {
        if (newTime - oldTime > 0.35f)
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playMovementSound();
            if (direct == "up")
            {
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical")*convertingScale, 0f);
                attemptMovement = "up";
                yPos += 1;
            }
            else if (direct == "down")
            {
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * convertingScale, 0f);
                attemptMovement = "down";
                yPos -= 1;
            }
            else if (direct == "left")
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * convertingScale, 0f, 0f);
                attemptMovement = "left";
                xPos -= 1;
            }
            else if (direct == "right")
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * convertingScale, 0f, 0f);
                attemptMovement = "right";
                xPos += 1;
            }
            piecePosition.addPlayerPos(attemptMovement);
            piecePosition.addBoxPos(attemptMovement);
            minorStepCounter++;
            if (minorStepCounter == times)
            {
                minorStepCounter = 0;
                TFlowController.nextStep();
            }
            oldTime = Time.time;
        }
    }
    /* Method Name: OnCollisionEnter2D(Collision2D col)
     * Summary: Rebound the player if the movement will crash into a wall or the border.
     * @param col: The Collision2D object of the GameObject the player collided with.
     * @return N/A
     * Special Effects: N/A
     */
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("StopMovement"))
        {
            rebound();
        }
    }
    /* Method Name: makeMovement()
     * Summary: Make movements according to the user input.
     * @param N/A
     * @return N/A
     * Special Effects: The method will update a variable to store the time when a movement is performed.
     */
    void makeMovement()
    {
        if ((Input.GetAxisRaw("Horizontal")) == 1f)
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playMovementSound();
            movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * convertingScale, 0f, 0f);
            attemptMovement = "right";
            xPos += 1;
        }
        else if ((Input.GetAxisRaw("Horizontal")) == -1f)
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playMovementSound();
            movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * convertingScale, 0f, 0f);
            attemptMovement = "left";
            xPos -= 1;
        }
        else if ((Input.GetAxisRaw("Vertical")) == 1f)
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playMovementSound();
            movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * convertingScale, 0f);
            attemptMovement = "up";
            yPos += 1;
        }
        else if ((Input.GetAxisRaw("Vertical")) == -1f)
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playMovementSound();
            movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") *convertingScale, 0f);
            attemptMovement = "down";
            yPos -= 1;
        }
        oldTime = Time.time;
    }
    /* Method Name: rebound()
     * Summary: Reverse the player's move and reverse the movement and position history.
     * @param N/A
     * @return N/A
     * Special Effects: N/A
     */
    public void rebound()
    {
        string lastMove = attemptMovement;
        reversePlayerMove(lastMove);
        piecePosition.backBoxPos();

    }
    /* Method Name: reversePlayerMove(string lastMove)
     * Summary: Update the move point position so that the last move could be reversed. Update the position of the player.
     * @param lastMove: The move that the player is trying to undo
     * @return N/A
     * Special Effects: N/A
     */
    public void reversePlayerMove(string lastMove)
    {
        if (lastMove == "up")
        {
            movePoint.position += new Vector3(0f, -convertingScale, 0f);
            yPos -= 1;
        }
        else if (lastMove == "down")
        {
            movePoint.position += new Vector3(0f, convertingScale, 0f);
            yPos += 1;
        }
        else if (lastMove == "left")
        {
            movePoint.position += new Vector3(convertingScale, 0f, 0f);
            xPos += 1;
        }
        else if (lastMove == "right")
        {
            movePoint.position += new Vector3(-convertingScale, 0f, 0f);
            xPos -= 1;
        }
        piecePosition.backPlayerPos();
    }
    /* Method Name: resetPlayer()
     * Summary: Move the player back to the original position and clean the movement and position history.
     * @param N/A
     * @return N/A
     * Special Effects: N/A
     */
    public void resetPlayer()
    {
        positionHistory = new List<List<int>>();
        positionHistory.Add(startingPosition);
        movementHistory = new ArrayList();
        transform.position = startingVectPosition;
        movePoint.position = startingVectPosition;
    }
}
