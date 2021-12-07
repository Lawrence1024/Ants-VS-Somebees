//FileName: FeatureButtonDetection.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: FeatureButtonDetection contains the functions that run when the buttons on a level are pressed.   
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FeatureButtonDetection : MonoBehaviour
{
    LevelManager levelManager;
    Loading loading;
    public GameObject tipButton;
    public GameObject gameCanvas;
    public GameObject[] buttons;
    GameObject showScoreBoardData;
    /* Method Name: Start()
     * Summary: Get the game object "LevelManager"'s script "LevelManager" (a script attatched to the LevelManager) and other game elements. 
     * @param N/A
     * @return N/A
     * Special Effects: Assign variables to different game elements.
     */
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        loading = levelManager.LoadingCanvas.transform.GetChild(0).GetComponent<Loading>();
        tipButton.SetActive(false);
        showScoreBoardData = levelManager.CelebratoryMessagesCanvas;
    }
    void Update()
    {
        
    }
    /* Method Name: activateTips()
     * Summary: When the tips button is pressed the tips canvas is activated.
     * @param N/A
     * @return N/A
     * Special Effects: Activite tips canvas.
     */
    public void activateTips() {
        levelManager.TipsCanvas.SetActive(true);
    }
    /* Method Name: executeExistButton(GameObject existObj)
     * Summary: When the exist button is pressed, it sets the existObj to deactivated. 
     * @param existObj: the parent object of the exist button. 
     * @return N/A
     * Special Effects: The parent object of the exist button is deactivated. 
     */
    public void executeExistButton(GameObject existObj)
    {
        existObj.SetActive(false);
    }
    /* Method Name: selectAnswer(GameObject answerButton)
     * Summary: When the answer button is pressed, checks if it is right or wrong. If it is wrong it plays a sound effect and calls
     *          minusHeart() in levelManager. If it is right it plays another sound effect and calls buffer();
     * @param answerButton: the answer button that was pressed 
     * @return N/A
     * Special Effects: Change the button color according to right or wrong.
     */
    public void selectAnswer(GameObject answerButton) {
        if (answerButton.GetComponent<ButtonRightOrWrong>().RightOrWrong == "wrong")
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playWrongSound();
            answerButton.GetComponent<Image>().color = new Vector4(1,0.39f,0.39f,1);
            answerButton.GetComponent<Button>().interactable = false;
            levelManager.minusHeart();
        }
        else {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playCorrectSound();
            answerButton.GetComponent<Image>().color = new Vector4(0.39f, 1, 0.39f, 1);
            StartCoroutine(buffer());
        }
    }
    /* Method Name: lastStep()
     * Summary: When the last step button is pressed, it calls whenHitBackButton() in gameCanvas and play a sound effect. 
     * @param N/A
     * @return N/A
     * Special Effects: Reverse one step.
     */
    public void lastStep()
    {
        if (GameObject.Find("Player").GetComponent<PlayerController>().movePointCloseEnough)
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playMovementSound();
            gameCanvas.GetComponent<PiecePosition>().whenHitBackButton();
        }
        
    }
    /* Method Name: resetBoard()
     * Summary: When the reset button is pressed, it calls whenHitResetButton() in gameCanvas and play a sound effect. 
     * @param N/A
     * @return N/A
     * Special Effects: Resetart the board.
     */
    public void resetBoard()
    {
        GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playMovementSound();
        gameCanvas.GetComponent<PiecePosition>().whenHitResetButton();
    }
    /* Method Name: goMap()
     * Summary: When the map button is pressed, it deactivated the score board celebratory message and call runLoading("Map") under
     *          the loading game object.
     * @param N/A
     * @return N/A
     * Special Effects: Scene switch.
     */
    public void goMap() {
        showScoreBoardData.SetActive(false);
        levelManager.LoadingCanvas.SetActive(true);
        loading.runLoading("Map");
    }
    /* Method Name: nextScene(string nextSceneName)
     * Summary: When the next scene button is pressed, it deactivated the score board celebratory message and call runLoading("Map") under
     *          the loading game object.
     * @param nextSceneName: The name of the scene to swith to. 
     * @return N/A
     * Special Effects: Scene switch.
     */
    public void nextScene(string nextSceneName) {
        showScoreBoardData.SetActive(false);
        levelManager.LoadingCanvas.SetActive(true);
        loading.runLoading(nextSceneName);
    }
    /* Method Name: buffer()
     * Summary: Set all the answer buttons to uninteractable and interactable after .5 seconds. Call answerCorrect() under PlayerController
     *          to see if this question is the last question to answered and if the level is passed. It also enable the movement of the player.
     * @param N/A
     * @return N/A
     * Special Effects: Answer buttons interactable chanaged, check win condition, player movement enabled. 
     */
    IEnumerator buffer() {
        levelManager.QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = false;
        levelManager.QuestionCanvas.transform.GetChild(2).gameObject.GetComponent<Button>().interactable = false;
        levelManager.QuestionCanvas.transform.GetChild(3).gameObject.GetComponent<Button>().interactable = false;
        levelManager.QuestionCanvas.transform.GetChild(4).gameObject.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(.5f);
        levelManager.QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = true;
        levelManager.QuestionCanvas.transform.GetChild(2).gameObject.GetComponent<Button>().interactable = true;
        levelManager.QuestionCanvas.transform.GetChild(3).gameObject.GetComponent<Button>().interactable = true;
        levelManager.QuestionCanvas.transform.GetChild(4).gameObject.GetComponent<Button>().interactable = true;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = true;
        }
        gameObject.GetComponent<LevelManager>().currentQuestionBox.GetComponent<BoxController>().answerCorrect();
        levelManager.QuestionCanvas.SetActive(false);
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
    }
    /* Method Name: quitProgram()
     * Summary: When the quit button is pressed, it calls this function to quit the application.  
     * @param N/A
     * @return N/A
     * Special Effects: The application is quit.
     */
    public void quitProgram()
    {
        Application.Quit();
    }
    /* Method Name: resumeGame()
    * Summary: When the resume button is pressed, it calls this function to deactive the pause menu and change the volume of the audio to 1.  
    * @param N/A
    * @return N/A
    * Special Effects: The game resumes.
    */
    public void resumeGame()
    {
        levelManager.PauseMenuCanvas.SetActive(false);
        GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(1f);
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
        GameObject.Find("PointsValue").GetComponent<PointsCalculation>().gamePause = false;
        StartCoroutine(GameObject.Find("PointsValue").GetComponent<PointsCalculation>().pointsCountDown());
    }
    /* Method Name: switchToMainMenu()
    * Summary: When the main menu button is pressed, it calls this function switch the current scene to the main menu. If the 
    *          current scene is the main menu, the game resumes.
    * @param N/A
    * @return N/A
    * Special Effects: The scene is switched or resume.
    */
    public void switchToMainMenu()
    {
        showScoreBoardData.SetActive(false);
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (GameObject.Find("AccountsManager") != null)
            {
                GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount.potentialStarsList[levelManager.level[0] * 3 + levelManager.level[1] - 4] = 3;
            }
            levelManager.LoadingCanvas.SetActive(true);
            levelManager.PauseMenuCanvas.SetActive(false);
            levelManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("MainMenu");
        }
        else
        {
            levelManager.PauseMenuCanvas.SetActive(false);
        }
    }
    /* Method Name: switchToMap()
    * Summary: When the map button is pressed, it calls this function switch the current scene to the map. If the current scene
    *          is the map, the game resumes.
    * @param N/A
    * @return N/A
    * Special Effects: The scene is switched or resume.
    */
    public void switchToMap()
    {
        showScoreBoardData.SetActive(false);
        if (SceneManager.GetActiveScene().name != "Map")
        {
            if (GameObject.Find("AccountsManager") != null)
            {
                GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount.potentialStarsList[levelManager.level[0] * 3 + levelManager.level[1] - 4] = 3;
            }
            levelManager.LoadingCanvas.SetActive(true);
            levelManager.PauseMenuCanvas.SetActive(false);
            levelManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("Map");
        }
        else
        {
            levelManager.PauseMenuCanvas.SetActive(false);
        }
    }

}
