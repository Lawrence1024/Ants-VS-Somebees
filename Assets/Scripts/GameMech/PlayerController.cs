//FileName: PlayerController.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: Controlls the player in each level. Keep track of all player information. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
    public List<List<int>> positionHistory=new List<List<int>>();
    public GameObject gameCanvas;
    private PiecePosition piecePosition;
    public string attemptMovement;
    public bool canMove = true;
    private float newTime;
    private float oldTime=0f;
    private float localScale = 107.8949f;
    public float convertingScale;
    public bool movePointCloseEnough;
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
        positionHistory.Add(new List<int> {xPos,yPos});
        startingPosition = new List<int> { xPos, yPos };
        startingVectPosition = transform.position;
        piecePosition = gameCanvas.GetComponent<PiecePosition>();
        convertingScale = findConvertingScale();
        movePointCloseEnough = true;
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
        movePointCloseEnough = state1;
        if (state1 && state2 && state3 && state4)
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
        movePoint.localPosition += new Vector3(localScale, 0f, 0f);
        float finalX = movePoint.transform.position.x;
        movePoint.localPosition += new Vector3(-localScale, 0f, 0f);
        return finalX - initialX;
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
            movePoint.localPosition += new Vector3(Input.GetAxisRaw("Horizontal") * localScale, 0f, 0f);
            attemptMovement="right";
            xPos += 1;
        }else if ((Input.GetAxisRaw("Horizontal")) == -1f)
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playMovementSound();
            movePoint.localPosition += new Vector3(Input.GetAxisRaw("Horizontal") * localScale, 0f, 0f);
            attemptMovement = "left";
            xPos -= 1;
        }
        else if ((Input.GetAxisRaw("Vertical")) == 1f)
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playMovementSound();
            movePoint.localPosition += new Vector3(0f, Input.GetAxisRaw("Vertical") * localScale, 0f);
            attemptMovement = "up";
            yPos += 1;
        }
        else if ((Input.GetAxisRaw("Vertical")) == -1f)
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playMovementSound();
            movePoint.localPosition += new Vector3(0f, Input.GetAxisRaw("Vertical") * localScale, 0f);
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
            movePoint.localPosition += new Vector3(0f, -localScale, 0f);
            yPos -= 1;
        }
        else if (lastMove == "down")
        {
            movePoint.localPosition += new Vector3(0f, localScale, 0f);
            yPos += 1;
        }
        else if (lastMove == "left")
        {
            movePoint.localPosition += new Vector3(localScale, 0f, 0f);
            xPos += 1;
        }
        else if (lastMove == "right")
        {
            movePoint.localPosition += new Vector3(-localScale, 0f, 0f);
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
