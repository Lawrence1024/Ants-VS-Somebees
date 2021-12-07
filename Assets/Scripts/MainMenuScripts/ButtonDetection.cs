//FileName: ButtonDetection.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: ButtonDetection contains the functions that run when the buttons on the main menu are pressed.  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonDetection : MonoBehaviour
{
    MainMenuManager mainMenuManager;
    public GameObject NextPageButton;
    public GameObject LastPageButton;
    public GameObject PageCount;
    public ScrollRect ScrollRect;
    int pageCounter = 0;
    /* Method Name: Start()
     * Summary: Get the game object "SceneManager"'s script "MainMenuManager" (a script attatched to the SceneManager) and store it in the 
     *          variable mainMenuManager. Set the leaderboard panel's position to the top. It sets the last page button on the instruciton 
     *          to deactive and the pagecount text to activate.
     * @param N/A
     * @return N/A
     * Special Effects: Leaderboard set to normal position, page count text activated, last page button deactivated. 
     */
    void Start()
    {
        mainMenuManager = GameObject.Find("SceneManager").GetComponent<MainMenuManager>();
        ScrollRect.verticalNormalizedPosition = 1f;
        if (pageCounter <= 0) {
            PageCount.SetActive(true);
            LastPageButton.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    /* Method Name: executePlayButton()
     * Summary: When the play button is pressed, it activates the loading canvas and call the function runLoading(string sceneName) 
     *          attatched to MainMenuManager with the scene name (Map) to switch to. 
     * @param N/A
     * @return N/A
     * Special Effects: The loading page shows and the game switch scene.
     */
    public void executePlayButton() {
        mainMenuManager.LoadingCanvas.SetActive(true);
        mainMenuManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("Map");
    }
    /* Method Name: executeInstructionButton()
     * Summary: When the instruction button is pressed, it sets the current intruction page to the first page (pageCounter=0) and 
     *          deactivate the last page button. It calls changeInstructionPate(int pageNum) in mainMenuManger with the current page 
     *          count. It activated the next page button. It change the text on the lower right corner of the instruction canvas 
     *          and set it to activate. Last, it sets the instruction canvas to activated.
     * @param N/A
     * @return N/A
     * Special Effects: Instruction canvas is activated. 
     */
    public void executeInstructionButton() {
        pageCounter = 0;
        LastPageButton.SetActive(false);
        mainMenuManager.changeInstrucitonPage(pageCounter);
        NextPageButton.SetActive(true);
        PageCount.GetComponent<TMPro.TextMeshProUGUI>().text = (pageCounter + 1) + "/"+ mainMenuManager.InstructionPages.Length;
        PageCount.SetActive(true);
        mainMenuManager.InstructionCanvas.SetActive(true);
    }
    /* Method Name: executeLeaderBoardButton()
     * Summary: When the leaderboard button is pressed, it activates the leaderbaord and call getAccountsTotalStars() in LeaderBoardCanvas,
     *          which sorts out the order of the rank. 
     * @param N/A
     * @return N/A
     * Special Effects: The leaderboard is activated. 
     */
    public void executeLeaderBoardButton() {
        ScrollRect.verticalNormalizedPosition = 1f;
        mainMenuManager.LeaderBoardCanvas.SetActive(true);
        mainMenuManager.LeaderBoardCanvas.GetComponent<LeaderBoardDisplay>().getAccountsTotalStars();
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
    /* Method Name: executeNextPageButton(GameObject existObj)
     * Summary: When the next page button is pressed, it plus 1 to the page counter. It detects if the instruciton is at its end so that
     *          the next button is deactivated, else activated. It calls changeInstructionPate(int pageNum) in mainMenuManger with the 
     *          current page count. It change the text on the lower right corner of the instruction canvas.
     * @param N/A
     * @return N/A
     * Special Effects: The instruction switch page (changeInstructionPage(int pageNum) does the job).  
     */
    public void executeNextPageButton()
    {
        pageCounter++;
        PageCount.SetActive(true);
        if (pageCounter > 0)
        {
            LastPageButton.SetActive(true);
        }
        if (pageCounter== (mainMenuManager.InstructionPages.Length-1)) {
            NextPageButton.SetActive(false);
        }
        mainMenuManager.changeInstrucitonPage(pageCounter);
        PageCount.GetComponent<TMPro.TextMeshProUGUI>().text = (pageCounter+1)+ "/" +mainMenuManager.InstructionPages.Length;
    }
    /* Method Name: executeLastPageButton(GameObject existObj)
     * Summary: When the last page button is pressed, it subtract 1 to the page counter. It detects if the instruciton is at its start so
     *          that the last button is deactivated, else activated. It calls changeInstructionPate(int pageNum) in mainMenuManger with the 
     *          current page count. It change the text on the lower right corner of the instruction canvas.
     * @param N/A
     * @return N/A
     * Special Effects: The instruction switch page (changeInstructionPage(int pageNum) does the job).  
     */
    public void executeLastPageButton()
    {
        pageCounter--;
        PageCount.SetActive(true);
        if (pageCounter <= 0)
        {
            LastPageButton.SetActive(false);
        }
        if (pageCounter < mainMenuManager.InstructionPages.Length-1)
        {
            NextPageButton.SetActive(true);
        }
        mainMenuManager.changeInstrucitonPage(pageCounter);
        PageCount.GetComponent<TMPro.TextMeshProUGUI>().text = (pageCounter + 1) + "/" + mainMenuManager.InstructionPages.Length;
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
    public void resumeGame() {
        mainMenuManager.PauseMenuCanvas.SetActive(false);
        GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(1f);
    }
    /* Method Name: switchToMainMenu()
    * Summary: When the main menu button is pressed, it calls this function switch the current scene to the main menu. If the 
    *          current scene is the main menu, the game resumes.
    * @param N/A
    * @return N/A
    * Special Effects: The scene is switched or resume.
    */
    public void switchToMainMenu() {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            mainMenuManager.LoadingCanvas.SetActive(true);
            mainMenuManager.PauseMenuCanvas.SetActive(false);
            mainMenuManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("MainMenu");
        }
        else {
            mainMenuManager.PauseMenuCanvas.SetActive(false);
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
        if (SceneManager.GetActiveScene().name != "Map")
        {
            mainMenuManager.LoadingCanvas.SetActive(true);
            mainMenuManager.PauseMenuCanvas.SetActive(false);
            mainMenuManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("Map");
        }
        else
        {
            mainMenuManager.PauseMenuCanvas.SetActive(false);
        }
    }
    /* Method Name: LogOut()
    * Summary: When the logout button is pressed, it sets the canvas except the main menu canvas to deactivated. It activated the 
    *          login panel in the game object "AccountsManager". The button is deactivated once it is pressed (activated once login).
    * @param LOBut: The logout button. 
    * @return N/A
    * Special Effects: The login panel is activated. 
    */
    public void LogOut(GameObject LOBut) {
        mainMenuManager.PauseMenuCanvas.SetActive(false);
        mainMenuManager.InstructionCanvas.SetActive(false);
        mainMenuManager.LoadingCanvas.SetActive(false);
        mainMenuManager.LeaderBoardCanvas.SetActive(false);
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<Button>().interactable=true;
        LOBut.SetActive(false);
    }
    /* Method Name: TutorialTransition()
    * Summary: When the tutorial button is pressed, it activates the loading canvas and call the function runLoading(string sceneName) 
     *          attatched to MainMenuManager with the scene name (Tutorial) to switch to. 
    * @param LOBut: The logout button. 
    * @return N/A
    * Special Effects: The loading page shows and the game switch scene.
    */
    public void TutorialTransition() {
        mainMenuManager.LoadingCanvas.SetActive(true);
        mainMenuManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("Tutorial");
    }
}
