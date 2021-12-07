//FileName: levelButtonDetection.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: levelButtonDetection contains the functions that run when the buttons on the map are pressed.   
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class levelButtonDetection : MonoBehaviour
{
    MapManager mapManager;
    /* Method Name: Start()
     * Summary: Get the game object "MapManager"'s script "MapManager" (a script attatched to the MapManager).
     * @param N/A
     * @return N/A
     * Special Effects: The script is saved to the variable mapManager.
     */
    void Start()
    {
        mapManager= GameObject.Find("MapManager").GetComponent<MapManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /* Method Name: changeLevel(GameObject levelButton)
     * Summary: Activate the loading page and call the function runLoading(string sceneName) attatched to MapManager with the name of
     *          the scene to switch to (e.g. Level_1_1). 
     * @param levelButton: the level button that is clicked on the map. The name of the button is the name of the scene, which is passed
     *          to runLoading(string sceneName).
     * @return N/A
     * Special Effects: The loading page shows and the game switch scene.
     */
    public void changeLevel(GameObject levelButton) {
        mapManager.LoadingCanvas.SetActive(true);
        mapManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading(levelButton.name);
    }
    public void changeLevel()
    {
        mapManager.LoadingCanvas.SetActive(true);
        mapManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("GamePlay");
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
        mapManager.PauseMenuCanvas.SetActive(false);
        GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(1f);
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
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            mapManager.LoadingCanvas.SetActive(true);
            mapManager.PauseMenuCanvas.SetActive(false);
            mapManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("MainMenu");
        }
        else
        {
            mapManager.PauseMenuCanvas.SetActive(false);
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
            mapManager.LoadingCanvas.SetActive(true);
            mapManager.PauseMenuCanvas.SetActive(false);
            mapManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("Map");
        }
        else
        {
            mapManager.PauseMenuCanvas.SetActive(false);
        }
    }
}
