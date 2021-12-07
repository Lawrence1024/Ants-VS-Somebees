//FileName: MapManager.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: MapManager contains the canvas (a type of game element in Unity) in the map, so when the canvas are deactivated, 
//             other scripts can still have access to the canvas and set them to activate. (You cannot use 
//             GameObject.Find("NameOfObject") to find a gameobject that is deactivated).
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public GameObject LoadingCanvas;
    public GameObject PauseMenuCanvas;
    public GameObject MapCanvas;
    public GameObject currentUserName;
    public GameObject currentStarCount;
    public GameObject userColor;
    public GameObject[] allLevelButtons;
    public GameObject[] starPanels;
    private Account activeAccount;
    /* Method Name: Start()
     * Summary: Set pause menu and loading canvas to deactivate when the map scene starts. Get the activateAccount through the 
     *          game object "AccountsManager" and see its progress. According to the progress, the starts and level buttons are 
     *          either activated/deactivated, interactable/uninteractable. It also sets the text display on the top left corner 
     *          on the screen to the user's name and the number of stars he/she has.
     * @param N/A
     * @return N/A
     * Special Effects: Pause menu and loading canvas deactivated. Star count and level bottons activated/deactivated, 
     *                  interactable/uninteractable. Display name and star count on the top left corner of the screen.
     */
    void Start()
    {
        PauseMenuCanvas.SetActive(false);
        LoadingCanvas.SetActive(false);
        activeAccount = GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount;
        for (int i=0; i<allLevelButtons.Length; i++) {
            allLevelButtons[i].GetComponent<Button>().interactable = false;
        }
        for (int i = 0; i < starPanels.Length; i++) {
            starPanels[i].transform.GetChild(0).gameObject.SetActive(false);
            starPanels[i].transform.GetChild(1).gameObject.SetActive(false);
            starPanels[i].transform.GetChild(2).gameObject.SetActive(false);
            starPanels[i].SetActive(false);
        }
        allLevelButtons[0].GetComponent<Button>().interactable = true;
        for (int i = 1; i < activeAccount.starsList.Count; i++)
        {
            if (activeAccount.starsList[i-1]>=0) {
                allLevelButtons[i].GetComponent<Button>().interactable = true;
            }
        }
        for (int i=0; i<activeAccount.starsList.Count;i++) {
            if (activeAccount.starsList[i]>=0) {
                for (int j=0; j<activeAccount.starsList[i]; j++) {
                    starPanels[i].transform.GetChild(j).gameObject.SetActive(true);
                }
                starPanels[i].SetActive(true);
            }
        }
        currentUserName.GetComponent<TMPro.TextMeshProUGUI>().text=activeAccount.userName;
        currentStarCount.GetComponent<TMPro.TextMeshProUGUI>().text = activeAccount.getTotalStar().ToString();
        userColor.GetComponent<Image>().color = activeAccount.avatarColor;
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
        if (!PauseMenuCanvas.activeSelf)
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(1f);
        }
    }
}
