//FileName: MainMenuManager.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: MainMenuManager contains the canvas (a type of game element in Unity) in the main menu, so when the canvas are 
//             deactivated, other scripts can still have access to the canvas and set them to activate. (You cannot use 
//             GameObject.Find("NameOfObject") to find a gameobject that is deactivated).
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject LoadingCanvas;
    public GameObject PauseMenuCanvas;
    public GameObject MainPageCanvas;
    public GameObject InstructionCanvas;
    public GameObject LeaderBoardCanvas;
    public GameObject UserNameInputBoxCanvas;
    public GameObject LogOutButton;
    public GameObject[] InstructionPages;
    public GameObject[] buttonsToDisableOnWarning;
    /* Method Name: Start()
     * Summary: Set all the unnecessary canvas to deactive.
     * @param N/A
     * @return N/A
     * Special Effects: The canvas are deactivated. 
     */
    void Start()
    {
        LoadingCanvas.SetActive(false);
        PauseMenuCanvas.SetActive(false);
        InstructionCanvas.SetActive(false);
        LeaderBoardCanvas.SetActive(false);
        MainPageCanvas.SetActive(true);
        UserNameInputBoxCanvas.transform.GetChild(2).gameObject.SetActive(false);
        UserNameInputBoxCanvas.transform.GetChild(3).gameObject.SetActive(false);
        UserNameInputBoxCanvas.transform.GetChild(4).gameObject.SetActive(false);
        for (int i = 0; i < InstructionPages.Length; i++)
        {
            InstructionPages[i].SetActive(false);
        }
        GameObject.Find("Counter").GetComponent<EnterMainMenuCounter>().IncreaseMainMenuCounter();
        if (GameObject.Find("Counter").GetComponent<EnterMainMenuCounter>().mainMenuCounter > 1)
        {
            LogOutButton.SetActive(true);
        }
        else {
            LogOutButton.SetActive(false);
        }
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
        PauseMenuCanvas.transform.GetChild(2).gameObject.GetComponent<Slider>().value=GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().sliderValue;
        if (!PauseMenuCanvas.activeSelf)
        {
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
        for (int i = 0; i < InstructionPages.Length; i++)
        {
            InstructionPages[i].SetActive(false);
        }
        InstructionPages[pageNum].SetActive(true);
    }
    /* Method Name: createAccount()
     * Summary: Activate the create account panel and deactivate the login panel and create account button. 
     * @param N/A
     * @return N/A
     * Special Effects: Activate/deactivate the login panel elements.
     */
    public void createAccount()
    {
        UserNameInputBoxCanvas.transform.GetChild(2).gameObject.SetActive(true);
        UserNameInputBoxCanvas.transform.GetChild(0).gameObject.SetActive(false);
        UserNameInputBoxCanvas.transform.GetChild(1).gameObject.SetActive(false);
    }
}
