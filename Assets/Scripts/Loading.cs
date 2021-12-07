//FileName: Loading.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: Loading contains the information to switch scenes.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public GameObject loadingGameObject;
    private string loadingText="Loading";
    private int loadingCounter = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    /* Method Name: runLoading(string sceneName)
     * Summary: Change the game volume to 0.25f and create a random number from 2 to 5 for the loading interval. It passes the scene's
     *          name to Buffer(string sceneName).
     * @param sceneName: The name of the scene to be switched to.
     * @return N/A
     * Special Effects: Decrease the volume to .25 and call Buffer(string sceneName).
     */
    public void runLoading(string sceneName) {
        GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(0.25f);
        loadingCounter = Random.Range(2, 5);
        StartCoroutine(Buffer(sceneName));        
    }
    /* Method Name: Buffer(string sceneName)
     * Summary: Wait for .5 seconds each interval to change the display of the loading text. Subtract 1 from loadingCounter each time
     *          until loadingCounter <= 0 to switch scene. 
     * @param sceneName: The name of the scene to be switched to.
     * @return N/A
     * Special Effects: Display the loading text animation and switch scene.
     */
    IEnumerator Buffer(string sceneName)
    {
        loadingCounter--;
        yield return new WaitForSeconds(.5f);
        if (loadingText.Length < 10)
        {
            loadingText = loadingText + ".";
            loadingGameObject.GetComponent<TMPro.TextMeshProUGUI>().text = loadingText;
        }
        else {
            loadingText = "Loading.";
            loadingGameObject.GetComponent<TMPro.TextMeshProUGUI>().text = loadingText;
        }
        if (loadingCounter > 0) {
            StartCoroutine(Buffer(sceneName));
        }
        if (loadingCounter <= 0)
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(1f);
            SceneManager.LoadScene(sceneName);
        }
    }
}

