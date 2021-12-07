//FileName: T_LevelManager.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: T_LevelManager contains the canvas (a type of game element in Unity) in the level, so when the canvas are 
//             deactivated, other scripts can still have access to the canvas and set them to activate. (You cannot use 
//             GameObject.Find("NameOfObject") to find a gameobject that is deactivated). (For tutorial).

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class T_LevelManager : MonoBehaviour
{
    public T_PointsCalculation pointsCalculation;
    public GameObject LoadingCanvas;
    public GameObject PauseMenuCanvas;
    public GameObject LevelCanvas;
    public GameObject TipsCanvas;
    public GameObject ScoreboardCanvas;
    public GameObject QuestionCanvas;
    public GameObject WarningCanvas;
    public GameObject FeatureCanvas;
    public GameObject currentQuestionBox;
    public GameObject playerSprite;
    public GameObject[] TipPages;
    public List<int> level;
    private Account activeAccount;
    private GameObject[] buttons;
    public GameObject TCanvas;
    public T_TutorialFlowController TFController;
    public bool garunteeCorrect = false;
    /* Method Name: Start()
     * Summary: Set all the unnecessary canvas to deactive. Change the player (moving box) color to the user's chosen avatar color. 
     * @param N/A
     * @return N/A
     * Special Effects: The canvas are deactivated. Color of player changes. 
     */
    void Start()
    {
        LoadingCanvas.SetActive(false);
        TipsCanvas.SetActive(false);
        ScoreboardCanvas.SetActive(false);
        QuestionCanvas.SetActive(false);
        WarningCanvas.SetActive(false);
        LevelCanvas.SetActive(true);
        PauseMenuCanvas.SetActive(false);
        activeAccount = GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount;
        pointsCalculation = GameObject.Find("PointsValue").GetComponent<T_PointsCalculation>();
        buttons = GameObject.FindGameObjectsWithTag("Buttons");
        TFController = TCanvas.GetComponent<T_TutorialFlowController>();
        displayStars("Stars");
        playerSprite.GetComponent<SpriteRenderer>().color = activeAccount.avatarColor;
    }
    /* Method Name: Update()
     * Summary: Detect if the escape key is pressed. If so, it calls activatePauseMenu().
     * @param N/A
     * @return N/A
     * Special Effects: Call activatePauseMenu().
     */
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            activatePauseMenu();
        }
    }
    /* Method Name: activatePauseMenu()
     * Summary: Activate (if the pause menu is not activated) or deactivate (if the pause men is activated) the pause menu. Also 4
     *          update the volume slider's value to the current volume. If the pasue menu is not activated it calls changeVolume(float val)
     *          in the game object "AudioPlayer" to set the volume to 1. 
     * @param N/A
     * @return N/A
     * Special Effects: Activate/deactivate the pause menu and change volume of background music.
     */
    void activatePauseMenu()
    {
        PauseMenuCanvas.SetActive(!PauseMenuCanvas.activeSelf);
        PauseMenuCanvas.transform.GetChild(2).gameObject.GetComponent<Slider>().value = GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().sliderValue;

        if (PauseMenuCanvas.activeSelf)
        {
            GameObject.Find("Player").GetComponent<T_PlayerController>().enabled = false;
            pointsCalculation.stopTime = true;
        }
        else
        {
            GameObject.Find("Player").GetComponent<T_PlayerController>().enabled = true;
            pointsCalculation.stopTime = false;
            StartCoroutine(GameObject.Find("PointsValue").GetComponent<PointsCalculation>().pointsCountDown());
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(1f);
        }
    }
    /* Method Name: changeInstrucitonPage(int pageNum)
     * Summary: Change the instruction page according to the input pageNum.
     * @param pageNum: the number of the page to be activated.
     * @return N/A
     * Special Effects: Activate/deactivate the instruction pages. 
     */
    public void changeInstrucitonPage(int pageNum)
    {
        for (int i = 0; i < TipPages.Length; i++)
        {
            TipPages[i].SetActive(false);
        }
        TipPages[pageNum].SetActive(true);
    }
    /* Method Name: minusHeart()
     * Summary: A heart is minus from the canvas. If all 3 hearts are lost, a star is minused. It calls loadWarning() to reload the 
     *          scene if all three hearts are lost. 
     * @param N/A
     * @return N/A
     * Special Effects: Change of heart count / star count on the canvas.
     */
    public void minusHeart()
    {
        if (GameObject.Find("Hearts").transform.childCount > 1)
        {
            GameObject.Find("Hearts").transform.GetChild(0).gameObject.SetActive(false);
            Destroy(GameObject.Find("Hearts").transform.GetChild(0).gameObject);
            activeAccount.tutorialFeatures[0]--;
            if (activeAccount.tutorialFeatures[1] == 1 && activeAccount.tutorialFeatures[0]==1)
            {
                garunteeCorrect = true;
                doGarunteeCorrect();
                TFController.instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "For the purpose of the tutorial, we will eliminate the wrong answers for you!";
            }
        }
        else if (GameObject.Find("Hearts").transform.childCount <= 1)
        {
            GameObject.Find("Hearts").transform.GetChild(0).gameObject.SetActive(false);
            Destroy(GameObject.Find("Hearts").transform.GetChild(0).gameObject);
            
            if (activeAccount.tutorialFeatures[1] > 0)
            {
                activeAccount.tutorialFeatures[1] --;
            }
            if (TFController.currentStep > 55)
            {
                if (activeAccount.tutorialFeatures[1] == 2)
                {
                    string s="You would only gain a maximum of 2 star in a real game, try to complete level without answering wrong!";
                    TFController.instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = s;
                    reloadScenePrep();
                    StartCoroutine(loadWarning("Level restart in", 3));
                }
                else if (activeAccount.tutorialFeatures[1] == 1)
                {
                    string s = "You would only gain a maximum of 1 star in a real game, try to complete level without answering wrong!";
                    TFController.instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = s; 
                    reloadScenePrep();
                    StartCoroutine(loadWarning("Level restart in", 3));
                }
            }
            else
            {
                if (activeAccount.tutorialFeatures[1] == 2)
                {
                    activeAccount.tutorialProgress[0] = true;
                }
            }
            activeAccount.tutorialFeatures[0] = 3;
            reloadScenePrep();
        }
    }
    //scene reload preperation
    public void reloadScenePrep()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
        }
        QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = false;
        QuestionCanvas.transform.GetChild(2).gameObject.GetComponent<Button>().interactable = false;
        QuestionCanvas.transform.GetChild(3).gameObject.GetComponent<Button>().interactable = false;
        QuestionCanvas.transform.GetChild(4).gameObject.GetComponent<Button>().interactable = false;
        pointsCalculation.enabled = false;
    }
    /* Method Name: displayScoreboard()
     * Summary: Display the scoreboard and set levelComplete in teh pointsCalculation script to true.
     * @param N/A
     * @return N/A
     * Special Effects: Scoreboard displayed. 
     */
    public void displayScoreboard()
    {
        pointsCalculation.levelComplete = true;
        ScoreboardCanvas.SetActive(true);
    }
    /* Method Name: displayScore()
     * Summary: Display the score you get in this level. Calls displayStars(string holdername).
     * @param N/A
     * @return N/A
     * Special Effects: Score displayed. 
     */
    public void displayScore()
    {
        GameObject.Find("FinalPoints").GetComponent<TMPro.TextMeshProUGUI>().text = "Points: " + GameObject.Find("PointsValue").GetComponent<TMPro.TextMeshProUGUI>().text;
        displayStars("FinalStarDisplay");
    }
    /* Method Name: displayStars()
     * Summary: Display the number of stars reamining onto the scoreboard. 
     * @param N/A
     * @return N/A
     * Special Effects: Stars displayed. 
     */
    public void displayStars(string holderName)
    {
        int starsRemain = activeAccount.tutorialFeatures[1];
        GameObject.Find(holderName).transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find(holderName).transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find(holderName).transform.GetChild(2).gameObject.SetActive(false);
        for (int i = 0; i < starsRemain; i++)
        {
            GameObject.Find(holderName).transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    /* Method Name: buffer()
     * Summary: Get the points value and star values of this level and store it in the active account's information. Display the rank
     *          on the score board
     * @param N/A
     * @return N/A
     * Special Effects: Stars displayed and rank displayed. 
     */
    public IEnumerator buffer()
    {
        pointsCalculation.levelComplete = true;
        activeAccount.tutorialProgress[1] = true;
        activeAccount.tutorialProgress[0] = false;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(loadWarning("You are ready to move on! Loading Main Menu in ", 5));
    }
    /* Method Name: displayMessage(string message, float sec)
     * Summary: Diplay a message for a certain amount of time.
     * @param message: The message to display.
     * @param sec: The duration that the message is displayed (in seconds).
     * @return an instance of WaitForSeconds which will allow the message to display for a certain duration.
     * Special Effects: User is notified that the tutorial doesn't contain a tips video.
     */
    public IEnumerator displayMessage(string message, float sec)
    {
        WarningCanvas.SetActive(true);
        WarningCanvas.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = message;
        yield return new WaitForSeconds(sec);
        WarningCanvas.SetActive(false);
    }
    /* Method Name: loadWarning(string warningMessage, float sec)
     * Summary: Display the warningMessage with the second given.
     * @param warningMessage: The information that is displayed. 
     * @param sec: The time to display. 
     * @return N/A
     * Special Effects: Warning message displayed
     */
    public IEnumerator loadWarning(string warningMessage, float sec)
    {
        if (sec < 1 && sec != 0)
        {
            WarningCanvas.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = warningMessage;
        }
        else
        {
            WarningCanvas.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = warningMessage + " " + sec;
        }
        WarningCanvas.SetActive(true);
        if (sec < 1 && sec != 0)
        {
            yield return new WaitForSeconds(sec);
            WarningCanvas.SetActive(false);
        }
        else if (sec >= 1 || sec <= 0)
        {
            yield return new WaitForSeconds(1);
            sec--;
            if (sec <= 0 && warningMessage == "Level restart in")
            {
                WarningCanvas.SetActive(false);
                reloadScene();
            } else if (sec <= 0) {
                LoadingCanvas.SetActive(true);
                GameObject.Find("LoadingBackground").GetComponent<Loading>().runLoading("MainMenu");
            }
            else if (sec > 0)
            {
                StartCoroutine(loadWarning(warningMessage, sec));
            }
        }
    }
    /* Method Name: reloadScene()
     * Summary: Get the current scene and reload.
     * @param N/A
     * @return N/A
     * Special Effects: Reload current scene.
     */
    void reloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    /* Method Name: doGarunteeCorrect()
     * Summary: When the user is at 1 heart and 1 star in the tutorial, block all the wrong answers.
     * @param N/A
     * @return N/A
     * Special Effects: User is garunteed to pass the tutorial level.
     */
    public void doGarunteeCorrect()
    {
        QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = true;
        QuestionCanvas.transform.GetChild(2).gameObject.GetComponent<Button>().interactable = false;
        QuestionCanvas.transform.GetChild(3).gameObject.GetComponent<Button>().interactable = false;
        QuestionCanvas.transform.GetChild(4).gameObject.GetComponent<Button>().interactable = false;
    }
}
