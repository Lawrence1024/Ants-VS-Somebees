//FileName: EndSceneManager.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: EndSceneManager controls the ending scene. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndSceneManager : MonoBehaviour
{
    AccountsManager accountManager;
    public GameObject creditText;
    public GameObject partyPopper1;
    public GameObject partyPopper2;
    public GameObject secondBackground;
    public GameObject TheEndText;
    public GameObject LoadingCanvas;
    public GameObject skipButton;
    private Vector4 secondBackgroundColor;
    private float convertingScale;
    private bool opacityChangeActivated=false;
    Vector3 stageDimensions;
    /* Method Name: Start()
     * Summary: Find the converting scale (different screen ration) and move the credit text to the bottom of the screen.
     * @param N/A
     * @return N/A
     * Special Effects: Object moved.
     */
    void Start()
    {
        accountManager = GameObject.Find("AccountsManager").GetComponent<AccountsManager>();
        convertingScale = findConvertingScale();
        stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        LoadingCanvas.SetActive(false);
        secondBackgroundColor = secondBackground.GetComponent<Image>().color;
        creditText.transform.localPosition = new Vector3(0f, -1605.4f, 0f);
        TheEndText.SetActive(false);
        setButton();
    }
    /* Method Name: Update()
    * Summary: If user pressed the esc key the program quits. The credit text moves up until hitting a upper boundery. When it hits the
    *          boundery decreaseOpacity() is called.
    * @param N/A
    * @return N/A
    * Special Effects: Object moved.
    */
    void Update()
    {
        if (Input.GetKeyDown("escape")) {
            Application.Quit();
        }
        creditText.transform.position = Vector3.MoveTowards(creditText.transform.position, new Vector3(0f, 24.62f*convertingScale, 0f), 4f* convertingScale * Time.deltaTime);
        if (Mathf.Abs(creditText.transform.position.y- 24.62f *convertingScale)<=0.05) {
            if (!opacityChangeActivated) {
                opacityChangeActivated = true;
                StartCoroutine(decreaseOpacity());

            }
        }
    }
    /* Method Name: setButton()
    * Summary: If the user already read the credit text then the skip button shows up.
    * @param N/A
    * @return N/A
    * Special Effects: Skip button shows up or not.
    */
    public void setButton() {
        if (accountManager.activeAccount.endSceneActivated)
        {
            skipButton.SetActive(true);
        }
        else
        {
            accountManager.activeAccount.endSceneActivated = true;
            accountManager.activeAccount.saveAccount();
            skipButton.SetActive(false);
        }
    }
    /* Method Name: findConvertingScale()
    * Summary: Find the converting scale between 16:9 and the user's aspect ratio.
    * @param N/A
    * @return finalX-initialX: The converting scale.
    * Special Effects: N/A
    */
    public float findConvertingScale()
    {
        float initialX = creditText.transform.position.x;
        creditText.transform.localPosition += new Vector3(107.8949f, 0f, 0f);
        float finalX = creditText.transform.position.x;
        creditText.transform.localPosition += new Vector3(-107.8949f, 0f, 0f);
        return finalX - initialX;
    }
    /* Method Name: decreaseOpacity()
    * Summary: Increase the opacity of a canvas that blocks the main canvas. 
    * @param N/A
    * @return N/A
    * Special Effects: Main canvas blocked
    */
    IEnumerator decreaseOpacity() {
        yield return new WaitForSeconds(0.1f);
        if (secondBackgroundColor[3]<1) {
            secondBackground.GetComponent<Image>().color = new Vector4(secondBackgroundColor[0], secondBackgroundColor[1], secondBackgroundColor[2], secondBackgroundColor[3] + 0.1f);
            secondBackgroundColor = secondBackground.GetComponent<Image>().color;
            StartCoroutine(decreaseOpacity());
        }
        else
        {
            StartCoroutine(displayTheEnd());
        }
        
    }
    /* Method Name: displayTheEnd()
    * Summary: After the main canvas is blocked the "Then End" text is displayed.  
    * @param N/A
    * @return N/A
    * Special Effects: Display "The End" text.
    */
    IEnumerator displayTheEnd() {
        TheEndText.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }
    /* Method Name: skip()
    * Summary: Skip the end scene if the skip button is pressed and return to the main menu. 
    * @param N/A
    * @return N/A
    * Special Effects: Return to main menu. 
    */
    public void skip() {
        LoadingCanvas.SetActive(true);
        LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("MainMenu");
    }
}
