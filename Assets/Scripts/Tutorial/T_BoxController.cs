//FileName: T_BoxController.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: T_BoxController is in charge of movements of boxes in the tutorial. Really similar to the BoxController class, just a little
//              modification based on the scenario of the tutorial.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_BoxController : MonoBehaviour
{
    T_LevelManager levelManager;
    public float moveSpeed = 20f;
    public Transform movePoint;
    public GameObject player;
    public LayerMask whatStopsMovement;
    public LayerMask boxLayer;
    public LayerMask playerLayer;
    public LayerMask questionLayer;
    public bool getPushed = false;
    private string lastPlayerMovement;
    public int xPos;
    public int yPos;
    public List<int> startingPosition;
    public List<List<int>> positionHistory = new List<List<int>>();
    public Vector3 startingVectPosition;
    public ArrayList movementHistory = new ArrayList();
    public GameObject gameCanvas;
    private PiecePosition piecePosition;
    public bool answered = false;
    public Sprite correctSprite;
    public float convertingScale;

    // Start is called before the first frame update
    /* Method Name: Start()
     * Summary: Find an initialize the starting position,find the converting scale, and basically initialize different values.
     * @param N/A
     * @return N/A
     * Special Effects: N/A
     */
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<T_LevelManager>();
        movePoint.position = transform.position;
        positionHistory.Add(new List<int> { xPos, yPos });
        startingPosition = new List<int> { xPos, yPos };
        startingVectPosition = transform.position;
        piecePosition = gameCanvas.GetComponent<PiecePosition>();
        convertingScale = player.GetComponent<T_PlayerController>().findConvertingScale();
    }

    // Update is called once per frame
    /* Method Name: Update()
    * Summary: For each frame of the game, have the sprite of the box move towards the box's move point.
    * @param N/A
    * @return N/A
    * Special Effects: Box is constantly moved to its move point.
    */
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        ifOverLap();
    }
    /* Method Name: ifOverLap()
    * Summary: Check if the player's position overlap with the box.
    * @param N/A
    * @return true if player position overlap, false if not
    * Special Effects: N/A
    */
    bool ifOverLap()
    {
        if (xPos == player.GetComponent<T_PlayerController>().xPos && yPos == player.GetComponent<T_PlayerController>().yPos)
        {
            return true;
        }
        return false;
    }
    /* Method Name: thereIsObstacle()
     * Summary: Check if there is any obstacle in the direction the box is trying to move towards.
     * @param N/A
     * @return true if there is obstacle, false if not.
     * Special Effects: N/A
     */
    bool thereIsObstacle()
    {
        bool obstUp = Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, convertingScale, 0f), 0.2f* convertingScale, whatStopsMovement);
        bool boxUp = Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, convertingScale, 0f), 0.2f* convertingScale, boxLayer);
        bool obstDown = Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -convertingScale, 0f), 0.2f* convertingScale, whatStopsMovement);
        bool boxDown = Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -convertingScale, 0f), 0.2f* convertingScale, boxLayer);
        bool obstLeft = Physics2D.OverlapCircle(movePoint.position + new Vector3(-convertingScale, 0f, 0f), 0.2f* convertingScale, whatStopsMovement);
        bool boxLeft = Physics2D.OverlapCircle(movePoint.position + new Vector3(-convertingScale, 0f, 0f), 0.2f* convertingScale, boxLayer);
        bool obstRight = Physics2D.OverlapCircle(movePoint.position + new Vector3(convertingScale, 0f, 0f), 0.2f* convertingScale, whatStopsMovement);
        bool boxRight = Physics2D.OverlapCircle(movePoint.position + new Vector3(convertingScale, 0f, 0f), 0.2f* convertingScale, boxLayer);
        if (lastPlayerMovement == "up" && (obstUp || boxUp))
        {
            return true;
        }
        else if (lastPlayerMovement == "down" && (obstDown || boxDown))
        {
            return true;
        }
        else if (lastPlayerMovement == "left" && (obstLeft || boxLeft))
        {
            return true;
        }
        else if (lastPlayerMovement == "right" && (obstRight || boxRight))
        {
            return true;
        }
        return false;
    }
    /* Method Name: move()
     * Summary: Call the makeMovement method and set getPushed to false
     * @param N/A
     * @return N/A
     * Special Effects: N/A
     */
    void move()
    {
        makeMovement();
        getPushed = false;
    }
    /* Method Name: makeMovement()
     * Summary: Base on the direction the box is getting pushed, move the movepoint accordingly. (Box will follow the move point).
     * @param N/A
     * @return N/A
     * Special Effects: The method will check if the player completed the level.
     */
    void makeMovement()
    {
        if (lastPlayerMovement == "up")
        {
            movePoint.position += new Vector3(0f, convertingScale, 0f);
            yPos += 1;
        }
        else if (lastPlayerMovement == "down")
        {
            movePoint.position += new Vector3(0f, -1 * convertingScale, 0f);
            yPos -= 1;
        }
        else if (lastPlayerMovement == "left")
        {
            movePoint.position += new Vector3(-1 * convertingScale, 0f, 0f);
            xPos -= 1;
        }
        else if (lastPlayerMovement == "right")
        {
            movePoint.position += new Vector3(convertingScale, 0f, 0f);
            xPos += 1;
        }
        GetComponentInParent<T_BoxManager>().checkIfWin();
    }
    /* Method Name: OnCollisionEnter2D(Collision2D col)
     * Summary: When the box is pushed, move when there is no obstacle. Rebound the player if there is obstacle.
     * @param col: The Collision2D object of the player.
     * @return N/A
     * Special Effects: N/A
     */
    void OnCollisionEnter2D(Collision2D col)
    {
        lastPlayerMovement = player.GetComponent<T_PlayerController>().attemptMovement;
        getPushed = true;
        if (thereIsObstacle())
        {
            player.GetComponent<T_PlayerController>().rebound();
        }
        else
        {
            move();
            positionHistory[positionHistory.Count - 1] = new List<int> { xPos, yPos };
            movementHistory[movementHistory.Count - 1] = lastPlayerMovement;
        }
        if (checkIfEnterQuestion() && !answered)
        {
            gameObject.GetComponentInParent<T_BoxManager>().checkIfWin();
            answerQuestion();
        }
    }
    /* Method Name: checkIfEnterQuestion()
     * Summary: Check if the box entered a question area (X area).
     * @param N/A
     * @return true if the box entered a question area, false if not.
     * Special Effects: N/A
     */
    public bool checkIfEnterQuestion()
    {
        if (Physics2D.OverlapCircle(movePoint.position, 0.2f, questionLayer))
        {
            return true;
        }
        return false;
    }
    /* Method Name: answerQuestion()
    * Summary: Activate the question panel.
    * @param N/A
    * @return N/A
    * Special Effects: N/A
    */
    public void answerQuestion()
    {
        gameObject.GetComponent<T_QuestionBoxCondition>().checkBoxQuestionStatus();
        levelManager.currentQuestionBox = gameObject;
        StartCoroutine(buffer());
    }
    /* Method Name: reverseBoxMove()
     * Summary: Activated when clicked the undo move button, move boxes to their last position.
     * @param N/A
     * @return N/A
     * Special Effects: The box remains unmoved if it wasn't pushed on the last move.
     */
    public void reverseBoxMove()
    {
        string lastMove = movementHistory[movementHistory.Count - 1].ToString();
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
        else if (lastMove == "-")
        {

        }
        else
        {
            Debug.Log("Error in reverseBoxMove in BoxController");
        }
    }
    /* Method Name: resetBox()
     * Summary: Clean up the position history and movement history of the box when the reset board button is pressed.
     * @param N/A
     * @return N/A
     * Special Effects: N/A
     */
    public void resetBox()
    {
        positionHistory = new List<List<int>>();
        positionHistory.Add(startingPosition);
        movementHistory = new ArrayList();
        transform.position = startingVectPosition;
        movePoint.position = startingVectPosition;
    }
    /* Method Name: answerCorrect()
     * Summary: Update the sprite and status of the box if the response of the question is correct.
     * @param N/A
     * @return N/A
     * Special Effects: The method will ask to check if the level is passed.
     */
    public void answerCorrect()
    {
        answered = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = correctSprite;
        gameObject.GetComponentInParent<T_BoxManager>().checkIfWin();
    }
    /* Method Name: buffer()
     * Summary: Update the sprite and status of the box if the response of the question is correct.
     * @param N/A
     * @return an instance of WaitForSecond, which will postpone the execution of implementation for 0.2 seconds.
     * Special Effects: N/A
     */
    IEnumerator buffer()
    {
        yield return new WaitForSeconds(.2f);
        GameObject.Find("Player").GetComponent<T_PlayerController>().enabled = false;
    }
}
