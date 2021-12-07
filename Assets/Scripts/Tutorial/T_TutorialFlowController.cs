//FileName: T_TutorialFlowController.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: T_TutorialFlowControllwer is the central control of the whold flow of the tutorial. It knows where in the tutorial we 
//              are at, and know what actions to take. It also blocks some unwanted actions in the tutorial.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T_TutorialFlowController : MonoBehaviour
{
    public GameObject gameCanvas;
    public T_PlayerController playerController;
    public T_BoxManager boxManager;
    public int currentStep = 0;
    public List<GameObject> answerButtons;
    public GameObject keysControlObj;
    public T_UI_KeysControl keysControl;
    public GameObject instructionText;
    public GameObject arrow;
    public Vector3 arrowGoToPosition;
    public bool arrowMovingToggle;
    public GameObject okButton;
    public T_LevelManager levelManager;
    public Account activeAccount;
    private float convertingScale;
    // Start is called before the first frame update
    /* Method Name: Start()
     * Summary: Initialize the value of the variables.
     * @param N/A
     * @return N/A
     * Special Effects: If the user have gone through the first part of the tutorial, move directly on to free play.
     */
    void Start()
    {
        activeAccount = GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount;
        playerController = gameCanvas.GetComponentInChildren<T_PlayerController>();
        boxManager = gameCanvas.GetComponentInChildren<T_BoxManager>();
        keysControl = keysControlObj.GetComponent<T_UI_KeysControl>();
        arrow.SetActive(false);
        arrowGoToPosition = new Vector3(0f, 0f, 0f);
        arrow.transform.position = new Vector3(0f, 0f, 0f);
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        arrowMovingToggle = true;
        okButton.SetActive(false);
        levelManager = GameObject.Find("LevelManager").GetComponent<T_LevelManager>();
        convertingScale=playerController.findConvertingScale();
        if (activeAccount.tutorialProgress[0])
        {
            currentStep = 58;
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Push the boxes into the X areas and complete questions!";
            if (activeAccount.tutorialFeatures[1] == 2)
            {
                instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "You would only gain a maximum of 2 star in a real game, try to complete level without answering wrong!";
            }
            else if (activeAccount.tutorialFeatures[1] == 1)
            {
                instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "You would only gain a maximum of 1 star in a real game, try to complete level without answering wrong!";
            }
        }

    }
    /* Method Name: Update()
     * Summary: For each frame, check if we are at a specific step. For some steps, move the pointing arrow every frame.
     * @param N/A
     * @return N/A
     * Special Effects: Arrows are displayed and animated on specific tutorial steps.
     */
    // Update is called once per frame
    void Update()
    {
        if (currentStep == 0)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Follow keyboard.\nPractice basic maneuver!";
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 18 || currentStep == 19 || currentStep == 20 || currentStep == 22 || currentStep == 23 || currentStep == 25 || currentStep == 44 || currentStep == 45 || (currentStep >= 46 && currentStep <= 54))
        {
            moveArrowLeftRight();
            arrow.transform.position = Vector3.MoveTowards(arrow.transform.position, arrowGoToPosition, 2f * Time.deltaTime* convertingScale);
        }
        else if (currentStep == 24)
        {
            moveArrowUpDown();
            arrow.transform.position = Vector3.MoveTowards(arrow.transform.position, arrowGoToPosition, 2f * Time.deltaTime* convertingScale);
        }
    }
    /* Method Name: moveArrowLeftRight()
     * Summary: Move the arrow image left and right.
     * @param N/A
     * @return N/A
     * Special Effects: N/A
     */
    public void moveArrowLeftRight()
    {
        if (Mathf.Abs(arrow.transform.position.x - arrowGoToPosition.x) < 0.05)
        {
            arrowMovingToggle = !arrowMovingToggle;
            if (arrowMovingToggle)
            {
                arrowGoToPosition = arrow.transform.position + new Vector3(convertingScale, 0f, 0f);
            }
            else
            {
                arrowGoToPosition = arrow.transform.position + new Vector3(-convertingScale, 0f, 0f);
            }
        }
    }
    /* Method Name: moveArrowUpDown()
     * Summary: Move the arrow up and down.
     * @param N/A
     * @return N/A
     * Special Effects: N/A
     */
    public void moveArrowUpDown()
    {
        if (Mathf.Abs(arrow.transform.position.y - arrowGoToPosition.y) < 0.05)
        {
            arrowMovingToggle = !arrowMovingToggle;
            if (arrowMovingToggle)
            {
                arrowGoToPosition = arrow.transform.position + new Vector3(0f, convertingScale, 0f);
            }
            else
            {
                arrowGoToPosition = arrow.transform.position + new Vector3(0f, -convertingScale, 0f);
            }
        }
    }
    /* Method Name: nextStep()
     * Summary: Add 1 to the current step and prepare for the next.
     * @param N/A
     * @return N/A
     * Special Effects: When a movement should be taken, the keys on the screen will glow.
     */
    public void nextStep()
    {
        currentStep++;
        respondToNext();
    }
    /* Method Name: respondToNext()
     * Summary: Base on the current step we are at, take a specific action.
     * @param N/A
     * @return N/A
     * Special Effects: Unwanted actions are blocked, user can only follow the tutorial.
     */
    public void respondToNext()
    {
        if (currentStep == 1)
        {
            keysControl.darkAllKeys();
            keysControl.glow("down");
        }
        else if (currentStep == 2)
        {
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 3)
        {
            keysControl.darkAllKeys();
            keysControl.glow("right");
        }
        else if (currentStep == 4)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Great! Now try to run through the wall to the right";
            keysControl.darkAllKeys();
            keysControl.glow("right");
        }
        else if (currentStep == 5)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "You can't pass walls, try to run through bottom border";
            keysControl.darkAllKeys();
            keysControl.glow("down");
        }
        else if (currentStep == 6)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "You can't leave border, let's go around the wall";
            keysControl.darkAllKeys();
            keysControl.glow("right");
        }
        else if (currentStep == 7)
        {
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 8)
        {
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 9)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Let's push the box to the left";
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 10)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Great! Let's try to push two boxes at the same time";
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 11)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Oops, you can't push two boxes at the same time. Go around the box";
            keysControl.darkAllKeys();
            keysControl.glow("down");
        }
        else if (currentStep == 12)
        {
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 13)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Let's push the box to upper border";
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 14)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Try push the box out of border";
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 15)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Oops, that didn't work.\n Let's go around.";
            keysControl.darkAllKeys();
            keysControl.glow("right");
        }
        else if (currentStep == 16)
        {
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 17)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Push the box into an X area";
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 18)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "A question will display.\n Click on a wrong answer.";
            arrow.SetActive(true);
            arrowMovingToggle = true;
            arrow.transform.position = new Vector3(2.146f * convertingScale, -1.733f* convertingScale, 0f);
            arrowGoToPosition = arrow.transform.position;
        }
        else if (currentStep == 19)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "You will lose a heart every time you answer wrong.\nClick on another wrong answer.";
            answerButtons[0].GetComponent<Button>().interactable = false;
            answerButtons[1].GetComponent<Button>().interactable = false;
            answerButtons[2].GetComponent<Button>().interactable = true;
            answerButtons[3].GetComponent<Button>().interactable = false;
            arrowMovingToggle = true;
            arrow.transform.position = new Vector3(2.146f * convertingScale, -2.966f* convertingScale, 0f);
            arrowGoToPosition = arrow.transform.position;
        }
        else if (currentStep == 20)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "1 heart left, let's click the right answer";
            answerButtons[0].GetComponent<Button>().interactable = true;
            answerButtons[1].GetComponent<Button>().interactable = false;
            answerButtons[2].GetComponent<Button>().interactable = false;
            answerButtons[3].GetComponent<Button>().interactable = false;
            arrowMovingToggle = true;
            arrow.transform.position = new Vector3(2.146f* convertingScale, -0.463f* convertingScale, 0f);
            arrowGoToPosition = arrow.transform.position;
        }
        else if (currentStep == 21)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Great! The box turned green! Let's push the other box to bottom border.";
            arrow.SetActive(false);
            answerButtons[0].GetComponent<Button>().interactable = true;
            answerButtons[1].GetComponent<Button>().interactable = true;
            answerButtons[2].GetComponent<Button>().interactable = true;
            answerButtons[3].GetComponent<Button>().interactable = true;
            keysControl.darkAllKeys();
            keysControl.glow("down");
        }
        else if (currentStep == 22)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Oops, we went to far. Click the button to undo last move.";
            arrowMovingToggle = true;
            arrow.transform.position = new Vector3(-5.059f* convertingScale, 3.318f* convertingScale, 0f);
            arrowGoToPosition = arrow.transform.position;
            arrow.SetActive(true);
            keysControl.darkAllKeys();
        }
        else if (currentStep == 23)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "You will lose 20 points every time you undo a move.";
            arrowMovingToggle = true;
            arrow.transform.position = new Vector3(-3.422f * convertingScale, 4.414f * convertingScale, 0f);
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            arrowGoToPosition = arrow.transform.position;
            arrow.SetActive(true);
            okButton.SetActive(true);
        }
        else if (currentStep == 24)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Let's reset the positions of the pieces with the other button.";
            arrow.transform.position = new Vector3(-8.212f* convertingScale, 1.744f* convertingScale, 0f);
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            arrowMovingToggle = true;
            arrowGoToPosition = arrow.transform.position;
            keysControl.darkAllKeys();
            arrow.SetActive(true);
            okButton.SetActive(false);
        }
        else if (currentStep == 25)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "You will lose 50 points every time you reset the positions of the pieces.";
            arrowMovingToggle = true;
            arrow.transform.position = new Vector3(-3.422f * convertingScale, 4.414f * convertingScale, 0f);
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            arrowGoToPosition = arrow.transform.position;
            arrow.SetActive(true);
            okButton.SetActive(true);
        }
        else if (currentStep == 26)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Let's try walking into one of the X areas.";
            keysControl.darkAllKeys();
            keysControl.glow("left");
            arrow.SetActive(false);
            okButton.SetActive(false);
        }
        else if (currentStep == 27)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Let's try walking into one of the X areas.";
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 28)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "As you can see, players can walk through the X areas.";
            keysControl.darkAllKeys();
            okButton.SetActive(true);
        }
        else if (currentStep == 29)
        {
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Let's move the green box back into the X area.";
            okButton.SetActive(false);
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 30)
        {
            keysControl.darkAllKeys();
            keysControl.glow("right");
        }
        else if (currentStep == 31)
        {
            keysControl.darkAllKeys();
            keysControl.glow("down");
        }
        else if (currentStep == 32)
        {
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 33)
        {
            keysControl.darkAllKeys();
            keysControl.glow("right");
        }
        else if (currentStep == 34)
        {
            keysControl.darkAllKeys();
            keysControl.glow("down");
        }
        else if (currentStep == 35)
        {
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 36)
        {
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 37)
        {
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 38)
        {
            keysControl.darkAllKeys();
            keysControl.glow("down");
        }
        else if (currentStep == 39)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "When the box is green, questions won't show up again.";
            okButton.SetActive(true);
            keysControl.darkAllKeys();
        }
        else if (currentStep == 40)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Push the green box out of the X.";
            okButton.SetActive(false);
            keysControl.darkAllKeys();
            keysControl.glow("down");
        }
        else if (currentStep == 41)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Let's push the other box onto an X.";
            keysControl.darkAllKeys();
            keysControl.glow("right");
        }
        else if (currentStep == 42)
        {
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 43)
        {
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 44)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Click on a wrong answer";
            arrow.transform.position = new Vector3(2.146f * convertingScale, -4.199f* convertingScale, 0f);
            arrowGoToPosition = arrow.transform.position;
            arrowMovingToggle = true;
            arrow.SetActive(true);
            keysControl.darkAllKeys();
        }
        else if (currentStep == 45)
        {
            okButton.SetActive(true);
            arrow.transform.position = new Vector3(4.087f* convertingScale, 4.477f* convertingScale, 0f);
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            arrowGoToPosition = arrow.transform.position;
            arrowMovingToggle = true;
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Oops, you just lost your last heart. The level will reset.";
            levelManager.QuestionCanvas.SetActive(false);
        }
        else if (currentStep == 46)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "When you lose all three hearts, you will lose a star";
            arrow.transform.position = new Vector3(4.087f* convertingScale, 3.401f* convertingScale, 0f);
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            arrowGoToPosition = arrow.transform.position;
            arrowMovingToggle = true;
            levelManager.displayStars("Stars");
        }
        else if (currentStep == 47)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "The first trial of each level will determine the stars you get";
        }
        else if (currentStep == 48)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "You can't replay a level to get more stars (since you would already know the answers)";
        }
        else if (currentStep == 49)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Stars will be used to determine your ranking on the leader board (on home page)";
        }
        else if (currentStep == 50)
        {
            arrow.transform.position = new Vector3(-3.422f* convertingScale, 4.414f* convertingScale, 0f);
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            arrowGoToPosition = arrow.transform.position;
            arrowMovingToggle = true;
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "In a normal game, complete the puzzles faster to get higher points.";
        }
        else if (currentStep == 51)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Points will determine your ranking in that level.";
        }
        else if (currentStep == 52)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "You can replay to score higher (It really all comes down to who can move their boxes faster)";
        }
        else if (currentStep == 53)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Your points will not get less than 0";
            okButton.SetActive(true);
        }
        else if (currentStep == 54)
        {
            arrow.transform.position = new Vector3(-4.822f * convertingScale, 2.314f * convertingScale, 0f);
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            arrowGoToPosition = arrow.transform.position;
            arrowMovingToggle = true;
            levelManager.GetComponent<T_FeatureButtonDetection>().tipButton.SetActive(true);
            levelManager.pointsCalculation.points = 0;
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "In a normal game, a tip button will show up after your points reach 0. If you are stuck, click on it to view a video about how to solve the level.";
        }
        else if (currentStep == 55)
        {
            levelManager.GetComponent<T_FeatureButtonDetection>().tipButton.SetActive(false);
            arrow.SetActive(false);
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Let's see if you can complete the tutorial level on your own.";
        }
        else if (currentStep == 56)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Good Luck And Enjoy!";
        }
        else if (currentStep == 57)
        {
            okButton.SetActive(false);
            activeAccount.tutorialFeatures[0] = 3;
            activeAccount.tutorialFeatures[1] = 3;
            StartCoroutine(levelManager.loadWarning("Level restart in", 3));
        }
        else if (currentStep == 59)
        {
            okButton.SetActive(true);
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Notice that you only win if the boxes are green AND all of them are on an X area.";
            levelManager.pointsCalculation.levelComplete = true;
        }
        else if (currentStep == 60)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "You can activate the pause menu and quit at anytime by pressing the ESC key.";
        }
        else if (currentStep == 61)
        {
            okButton.SetActive(false);
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Great job on completing the tutorial.";
            activeAccount.tutorialFeatures[0] = 3;
            activeAccount.tutorialFeatures[1] = 3;
            StartCoroutine(levelManager.buffer());
        }
    }
}
