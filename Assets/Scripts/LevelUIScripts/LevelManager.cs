//FileName: LevelManager.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: LevelManager contains the canvas (a type of game element in Unity) in the level, so when the canvas are 
//             deactivated, other scripts can still have access to the canvas and set them to activate. (You cannot use 
//             GameObject.Find("NameOfObject") to find a gameobject that is deactivated).
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    PointsCalculation pointsCalculation;
    FeatureButtonDetection featureButtonDetection;
    public GameObject LoadingCanvas;
    public GameObject PauseMenuCanvas;
    public GameObject LevelCanvas;
    public GameObject TipsCanvas;
    public GameObject ScoreboardCanvas;
    public GameObject QuestionCanvas;
    public GameObject WarningCanvas;
    public GameObject FeatureCanvas;
    public GameObject CelebratoryMessagesCanvas;
    public GameObject currentQuestionBox;
    public GameObject playerSprite;
    public GameObject pointsValue;
    public GameObject[] TipPages;
    public GameObject[] buttons;
    public List<int> level;
    private Account activeAccount;
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
        PauseMenuCanvas.SetActive(false);
        LevelCanvas.SetActive(false);
        FeatureCanvas.SetActive(false);
        activeAccount = GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount;
        featureButtonDetection = GetComponent<FeatureButtonDetection>();
        pointsCalculation = pointsValue.GetComponent<PointsCalculation>();
        //displayStars("Stars");
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

        if (PauseMenuCanvas.activeSelf) {
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
            pointsCalculation.gamePause = true;
        }
        else{
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
            pointsCalculation.gamePause = false;
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
    public void minusHeart() {
        if (GameObject.Find("Hearts").transform.childCount>1) {
            GameObject.Find("Hearts").transform.GetChild(0).gameObject.SetActive(false);
            Destroy(GameObject.Find("Hearts").transform.GetChild(0).gameObject);
        }
        else if(GameObject.Find("Hearts").transform.childCount <=1)
        {
            if(0 > activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4] - 1)
            {
                activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4] = 0;
            }
            else
            {
                activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4] = activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4] - 1;
            }
            activeAccount.saveAccount();
            GameObject.Find("Hearts").transform.GetChild(0).gameObject.SetActive(false);
            Destroy(GameObject.Find("Hearts").transform.GetChild(0).gameObject);

            //scene reload
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Button>().interactable = false;
            }
            QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = false;
            QuestionCanvas.transform.GetChild(2).gameObject.GetComponent<Button>().interactable = false;
            QuestionCanvas.transform.GetChild(3).gameObject.GetComponent<Button>().interactable = false;
            QuestionCanvas.transform.GetChild(4).gameObject.GetComponent<Button>().interactable = false;
            pointsCalculation.gamePause = true;
            StartCoroutine(loadWarning("Level restart in", 3));
        }
    }
    /* Method Name: displayScoreboard()
     * Summary: Display the scoreboard and set levelComplete in the pointsCalculation script to true.
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
    public void displayScore() {
        GameObject.Find("FinalPoints").GetComponent<TMPro.TextMeshProUGUI>().text = "Points: "+GameObject.Find("PointsValue").GetComponent<TMPro.TextMeshProUGUI>().text;
        //displayStars("FinalStarDisplay");
    }
    /* Method Name: displayStars()
     * Summary: Display the number of stars reamining onto the scoreboard. 
     * @param N/A
     * @return N/A
     * Special Effects: Stars displayed. 
     */
    public void displayStars(string holderName) {
        int starsRemain = activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4];
        GameObject.Find(holderName).transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find(holderName).transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find(holderName).transform.GetChild(2).gameObject.SetActive(false);
        for (int i = 0; i < starsRemain; i++) {
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
        if(activeAccount.starsList[level[0] * 3 + level[1] - 4] == -1)
        {
            activeAccount.starsList[level[0] * 3 + level[1] - 4] = activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4];
        }
        //hard code -1
        if (activeAccount.pointsList[level[0] * 3 + level[1] - 4]< int.Parse(GameObject.Find("PointsValue").GetComponent<TMPro.TextMeshProUGUI>().text))
        {
            if (int.Parse(GameObject.Find("PointsValue").GetComponent<TMPro.TextMeshProUGUI>().text) == 0)
            {
                activeAccount.pointsList[level[0] * 3 + level[1] - 4] = 0;
            }
            else
            {
                activeAccount.pointsList[level[0] * 3 + level[1] - 4] = int.Parse(GameObject.Find("PointsValue").GetComponent<TMPro.TextMeshProUGUI>().text) - 1;
            }
        }
        activeAccount.saveAccount();
        
        
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
        }
        yield return new WaitForSeconds(1f);
        displayScoreboard();
        displayScore();
        ScoreboardCanvas.GetComponent<ShowScoreBoardData>().getAccountsPoints(level);
        activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4] = 3;
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
        if (sec < 1&&sec!=0)
        {
            WarningCanvas.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = warningMessage;
        }
        else {
            WarningCanvas.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = warningMessage+" "+sec;
        }
        WarningCanvas.SetActive(true);
        if (sec < 1 && sec != 0)
        {
            yield return new WaitForSeconds(sec);
            WarningCanvas.SetActive(false);
        }
        else if (sec >= 1||sec<=0) {
            yield return new WaitForSeconds(1);
            sec--;
            if (sec <= 0) {
                WarningCanvas.SetActive(false);
                //reloadScene();
                LoadingCanvas.SetActive(true);
                LoadingCanvas.GetComponentInChildren<Loading>().runLoading("Map");
            } else if(sec>0){
                StartCoroutine(loadWarning(warningMessage, sec));
            }
        }
    }
    public IEnumerator loadWarning(string warningMessage, float sec, bool lastLevel)
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
            if (sec <= 0)
            {
                WarningCanvas.SetActive(false);
                //reloadScene();
                LoadingCanvas.SetActive(true);
                if (lastLevel)
                {
                    LoadingCanvas.GetComponentInChildren<Loading>().runLoading("EndScene");
                }
                else
                {
                    LoadingCanvas.GetComponentInChildren<Loading>().runLoading("Map");
                }
                
            }
            else if (sec > 0)
            {
                StartCoroutine(loadWarning(warningMessage, sec, true));
            }
        }
    }
    /* Method Name: reloadScene()
     * Summary: Get the current scene and reload.
     * @param N/A
     * @return N/A
     * Special Effects: Reload current scene.
     */
    void reloadScene() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
