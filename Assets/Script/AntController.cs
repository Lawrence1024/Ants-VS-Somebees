//FileName: BoxController.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: BoxController is in charge of movements of boxes in the game.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : InsectController
{
    //LevelManager levelManager;
    public float moveSpeed = 20f;
    //public GameObject player;
    public LayerMask whatStopsMovement;
    public LayerMask boxLayer;
    public LayerMask playerLayer;
    public LayerMask questionLayer;
    public bool getPushed = false;
    private string lastPlayerMovement;
    public List<int> startingPosition;
    //public List<List<int>> positionHistory = new List<List<int>>();
    public Vector3 startingVectPosition;
    public GameObject gameCanvas;
    public Transform movePoint;
    public string antName = "Ant";
    public List<GameObject> peas;
    public int foodCost;
    public bool isContainer;
    public AntController antContained = null;
    public bool buffed;
    //public ArrayList movementHistory = new ArrayList();
    //private PiecePosition piecePosition;
    //public bool answered = false;
    //public Sprite correctSprite;
    // Start is called before the first frame update
    /* Method Name: Start()
     * Summary: Find an initialize the starting position,find the converting scale, and basically initialize different values.
     * @param N/A
     * @return N/A
     * Special Effects: N/A
     */

    //public new int test = 3;

    public override void Awake()
    {
        base.Awake();
        health = 1;
        damage = 0;
        foodCost = 1;
        isContainer = false;
        buffed = false;
    }


    public override void Start()
    {
        base.Start();
        GetComponent<RectTransform>().localPosition = new Vector3(xPos * localScale + localScale / 2f, yPos * localScale + localScale / 2f, 0f);
        movePoint = transform.parent.Find("AntMovePoint");
        movePoint.position = transform.position;
        //levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        //positionHistory.Add(new List<int> { xPos, yPos });
        startingPosition = new List<int> { xPos, yPos };
        startingVectPosition = transform.position;
        movePoint.position = transform.position;
        //piecePosition = gameCanvas.GetComponent<PiecePosition>();
        //convertingScale = player.GetComponent<PlayerController>().findConvertingScale();



    }
    // Update is called once per frame
    /* Method Name: Update()
    * Summary: For each frame of the game, have the sprite of the box move towards the box's move point.
    * @param N/A
    * @return N/A
    * Special Effects: Box is constantly moved to its move point.
    */
    public override void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        timeTrack();
        //ifOverLap();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        try
        {
            if (col.collider.CompareTag("Bees") && !col.otherCollider.CompareTag("Pea") && place.ant == this)
            {
                GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playWrongSound();
                BeeController attackingBee = col.collider.GetComponent<BeeController>();
                reduceHealth(attackingBee.getDamage());
            }
        }
        catch
        {

        }
        
    }

    //    /* Method Name: ifOverLap()
    //     * Summary: Check if the player's position overlap with the box.
    //     * @param N/A
    //     * @return true if player position overlap, false if not
    //     * Special Effects: N/A
    //     */
    //    bool ifOverLap()
    //    {
    //        if(xPos == player.GetComponent<PlayerController>().xPos && yPos == player.GetComponent<PlayerController>().yPos)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }
    //    /* Method Name: thereIsObstacle()
    //     * Summary: Check if there is any obstacle in the direction the box is trying to move towards.
    //     * @param N/A
    //     * @return true if there is obstacle, false if not.
    //     * Special Effects: N/A
    //     */
    //    bool thereIsObstacle()
    //    {
    //        bool obstUp = Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, convertingScale, 0f), 0.2f* convertingScale, whatStopsMovement);
    //        bool boxUp = Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, convertingScale, 0f), 0.2f* convertingScale, boxLayer);
    //        bool obstDown = Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -convertingScale, 0f), 0.2f* convertingScale, whatStopsMovement);
    //        bool boxDown = Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -convertingScale, 0f), 0.2f* convertingScale, boxLayer);
    //        bool obstLeft = Physics2D.OverlapCircle(movePoint.position + new Vector3(-convertingScale, 0f, 0f), 0.2f* convertingScale, whatStopsMovement);
    //        bool boxLeft = Physics2D.OverlapCircle(movePoint.position + new Vector3(-convertingScale, 0f, 0f), 0.2f* convertingScale, boxLayer);
    //        bool obstRight = Physics2D.OverlapCircle(movePoint.position + new Vector3(convertingScale, 0f, 0f), 0.2f* convertingScale, whatStopsMovement);
    //        bool boxRight = Physics2D.OverlapCircle(movePoint.position + new Vector3(convertingScale, 0f, 0f), 0.2f* convertingScale, boxLayer);
    //        if (lastPlayerMovement=="up"&& (obstUp || boxUp))
    //        {
    //            return true;
    //        }else if (lastPlayerMovement == "down" && (obstDown || boxDown))
    //        {
    //            return true;
    //        }
    //        else if (lastPlayerMovement == "left" && (obstLeft || boxLeft))
    //        {
    //            return true;
    //        }
    //        else if (lastPlayerMovement == "right" && (obstRight || boxRight))
    //        {
    //            return true;
    //        }
    //        return false;
    //    }
    //    /* Method Name: move()
    //     * Summary: Call the makeMovement method and set getPushed to false
    //     * @param N/A
    //     * @return N/A
    //     * Special Effects: N/A
    //     */
    //    void move()
    //    {
    //        makeMovement();
    //        getPushed = false;
    //    }
    //    /* Method Name: makeMovement()
    //     * Summary: Base on the direction the box is getting pushed, move the movepoint accordingly. (Box will follow the move point).
    //     * @param N/A
    //     * @return N/A
    //     * Special Effects: The method will check if the player completed the level.
    //     */
    //    void makeMovement()
    //    {
    //        if (lastPlayerMovement=="up")
    //        {
    //            movePoint.localPosition += new Vector3(0f, localScale, 0f);
    //            yPos += 1;
    //        }else if (lastPlayerMovement == "down")
    //        {
    //            movePoint.localPosition += new Vector3(0f, -1* localScale, 0f);
    //            yPos -= 1;
    //        }else if (lastPlayerMovement == "left")
    //        {
    //            movePoint.localPosition += new Vector3(-1* localScale, 0f, 0f);
    //            xPos -= 1;
    //        }else if (lastPlayerMovement == "right")
    //        {
    //            movePoint.localPosition += new Vector3(localScale, 0f, 0f);
    //            xPos += 1;
    //        }
    //        GetComponentInParent<BoxManager>().checkIfWin();
    //    }
    //    /* Method Name: OnCollisionEnter2D(Collision2D col)
    //     * Summary: When the box is pushed, move when there is no obstacle. Rebound the player if there is obstacle.
    //     * @param col: The Collision2D object of the player.
    //     * @return N/A
    //     * Special Effects: N/A
    //     */
    //    void OnCollisionEnter2D(Collision2D col)
    //    {
    //        lastPlayerMovement = player.GetComponent<PlayerController>().attemptMovement;
    //        getPushed = true;
    //        if (thereIsObstacle())
    //        {
    //            player.GetComponent<PlayerController>().rebound();
    //        }
    //        else
    //        {
    //            move();
    //            positionHistory[positionHistory.Count - 1] = new List<int> { xPos, yPos };
    //            movementHistory[movementHistory.Count - 1] = lastPlayerMovement;
    //        }
    //        if (checkIfEnterQuestion()&&!answered)
    //        {
    //            gameObject.GetComponentInParent<BoxManager>().checkIfWin();
    //            answerQuestion();
    //        }
    //    }
    //    /* Method Name: checkIfEnterQuestion()
    //     * Summary: Check if the box entered a question area (X area).
    //     * @param N/A
    //     * @return true if the box entered a question area, false if not.
    //     * Special Effects: N/A
    //     */
    //    public bool checkIfEnterQuestion()
    //    {
    //        if(Physics2D.OverlapCircle(movePoint.position, 0.2f, questionLayer))
    //        {
    //            return true;
    //        }
    //        return false;
    //    }
    //    /* Method Name: answerQuestion()
    //     * Summary: Activate the question panel.
    //     * @param N/A
    //     * @return N/A
    //     * Special Effects: N/A
    //     */
    //
    //    public void answerQuestion()
    //    {
    //        gameObject.GetComponent<QuestionBoxCondition>().checkBoxQuestionStatus();
    //        levelManager.currentQuestionBox = gameObject;
    //        StartCoroutine(buffer());
    //    }
    //    /* Method Name: reverseBoxMove()
    //     * Summary: Activated when clicked the undo move button, move boxes to their last position.
    //     * @param N/A
    //     * @return N/A
    //     * Special Effects: The box remains unmoved if it wasn't pushed on the last move.
    //     */
    //    public void reverseBoxMove()
    //    {
    //        string lastMove = movementHistory[movementHistory.Count - 1].ToString();
    //        if (lastMove == "up")
    //        {
    //            movePoint.localPosition += new Vector3(0f, -localScale, 0f);
    //            yPos -= 1;
    //        }
    //        else if (lastMove == "down")
    //        {
    //            movePoint.localPosition += new Vector3(0f, localScale, 0f);
    //            yPos += 1;
    //        }
    //        else if (lastMove == "left")
    //        {
    //            movePoint.localPosition += new Vector3(localScale, 0f, 0f);
    //            xPos += 1;
    //        }
    //        else if (lastMove == "right")
    //        {
    //            movePoint.localPosition += new Vector3(-localScale, 0f, 0f);
    //            xPos -= 1;
    //        }else if (lastMove == "-")
    //        {
    //
    //        }
    //        else
    //        {
    //            Debug.Log("Error in reverseBoxMove in BoxController");
    //        }
    //    }
    //    /* Method Name: resetBox()
    //     * Summary: Clean up the position history and movement history of the box when the reset board button is pressed.
    //     * @param N/A
    //     * @return N/A
    //     * Special Effects: N/A
    //     */
    //    public void resetBox()
    //    {
    //        positionHistory = new List<List<int>>();
    //        positionHistory.Add(startingPosition);
    //        movementHistory = new ArrayList();
    //        transform.position = startingVectPosition;
    //        movePoint.position = startingVectPosition;
    //    }
    //    /* Method Name: answerCorrect()
    //     * Summary: Update the sprite and status of the box if the response of the question is correct.
    //     * @param N/A
    //     * @return N/A
    //     * Special Effects: The method will ask to check if the level is passed.
    //     */
    //    public void answerCorrect()
    //    {
    //        answered = true;
    //        gameObject.GetComponent<SpriteRenderer>().sprite = correctSprite;
    //        gameObject.GetComponentInParent<BoxManager>().checkIfWin();
    //    }
    //    /* Method Name: buffer()
    //     * Summary: Update the sprite and status of the box if the response of the question is correct.
    //     * @param N/A
    //     * @return an instance of WaitForSecond, which will postpone the execution of implementation for 0.2 seconds.
    //     * Special Effects: N/A
    //     */
    //    IEnumerator buffer()
    //    {
    //        yield return new WaitForSeconds(.2f);
    //        GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
    //    }
    public override bool addTo(TileData tile)
    {
        if(antName == "Queen" && GameState.hasQueen)
        {
            return false;
        }
        if (tile.ant == null)
        {
            tile.ant = this;
            base.addTo(tile);
        }
        else
        {
            AntController otherAnt = tile.ant;
            if (isContainer && canContain(otherAnt))
            {
                storeAnt(otherAnt);
                tile.ant = this;
                base.addTo(tile);
            }
            else if(otherAnt.isContainer && otherAnt.canContain(this))
            {
                otherAnt.storeAnt(this);
                base.addTo(tile);
            }
            else
            {
                Debug.Log("Cannot stack ants.");
                return false;
            }
        }
        return true;
    }
    public override void removeFrom(TileData tile)
    {
        if(tile.ant == this)
        {
            tile.ant = null;
        }else if(tile.ant == null)
        {
            Debug.Log(name + " is not in " + tile.cord);
        }
        else
        {
            tile.ant.removeAnt(this);
        }
        base.removeFrom(tile);
    }
    public override void nextRound()
    {
        base.nextRound();
        //peaSpawner.spawnPeas(1);
    }

    public override void reduceHealth(float amount)
    {
        base.reduceHealth(amount);
        if (health <= 0)
        {
            removeFrom(place);
        }
    }

    public virtual bool canContain(AntController other)
    {
        return false;
    }

    public virtual void storeAnt(AntController other)
    {
        Debug.Log(name+" cannot contain an ant.");
    }

    public virtual void removeAnt(AntController other)
    {
        Debug.Log(name + " cannot contain an ant.");
    }

    public void buff()
    {
        if(!buffed)
        {
            damage *= 2;
            buffed = true;
        }
    }
}
