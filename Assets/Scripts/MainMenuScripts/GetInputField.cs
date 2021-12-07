//FileName: GetInputField.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: GetInputField contains the functions on the inputfield of login and create account.  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GetInputField : MonoBehaviour
{
    MainMenuManager mainMenuManager;
    public bool accountTaken=false;
    public bool accountExist = false;
    public GameObject accManagerObj;
    private AccountsManager accManager;
    private string warningNote = "";
    /* Method Name: Start()
     * Summary: Get the game object "SceneManager"'s script "MainMenuManager" (a script attatched to the SceneManager). Get the current
     *          active account.
     * @param N/A
     * @return N/A
     * Special Effects: The script is saved to the variable mainMenuManager. The active account is saved to the variable accManager.
     */
    void Start()
    {
        mainMenuManager = GameObject.Find("SceneManager").GetComponent<MainMenuManager>();
        accManager = accManagerObj.GetComponent<AccountsManager>();
    }
    void Update()
    {
        
    }
    /* Method Name: GetName(GameObject userName)
     * Summary: Get the account's info using the text inside the userName game object. If the account doesn't exist a warning sign is
     *          displayed. 
     * @param userName: The input field of the text. 
     * @return N/A
     * Special Effects: Warning sign display or not.
     */
    public void GetName(GameObject userName)
    {
        string inputText = userName.GetComponent<TMPro.TextMeshProUGUI>().text;
        if (string.IsNullOrEmpty(inputText) || inputText.Length==1)
        {
            return;
        }
        else {
            accManager.loadAccount(userName);
            accountExist = accManager.checkIfAccountExist(inputText);
            if (!accountExist)
            {
                warningNote = "Account does not exist! Please create a new account";
                StartCoroutine(displayWarning());
            }
        }
    }
    /* Method Name: GetPassword(GameObject userPassword)
     * Summary: Get the account's info using the text inside the userName game object. If the account's input password does not match
     *          with the account's real password a warning is display. If the sign in username and password are correct, a sign is 
     *          also display.
     * @param userPassword: The input field of the text. 
     * @return N/A
     * Special Effects: Warning sign display or not.
     */
    public void GetPassword(GameObject userPassword) {
        string password = userPassword.GetComponent<TMPro.TextMeshProUGUI>().text;
        //Get the username bank and check if there's duplicate, if account already exist, accountTaken=true;
        if (string.IsNullOrEmpty(password) || password.Length == 1)
        {
            if (warningNote!= "Account does not exist! Please create a new account") {
                warningNote = "Incorrect Password!";
                StartCoroutine(displayWarning());
            }
            return;
        }
        else
        {
            password = password.Substring(0, password.Length - 1);
            bool successLogin = accManager.confirmLogin(password);
            if (!successLogin)
            {
                if (warningNote != "Account does not exist! Please create a new account")
                {
                    warningNote = "Incorrect Password!";
                    StartCoroutine(displayWarning());
                }
            }
            else
            {
                warningNote = "Sign in Successfully!";
                StartCoroutine(displayWarning());
            }
        }
    }
    /* Method Name: createAccount(GameObject UserNameInputBoxCanvas)
     * Summary: If the create new account button is clicked, it sets the login page and create new account button to deactivated 
     *          and the create new account page to activated. 
     * @param UserNameInputBoxCanvas: The parent of the new account button. (All things related to login is under this parent).
     * @return N/A
     * Special Effects: Deactivate login page and new account button, activate new account page.
     */
    public void createAccount(GameObject UserNameInputBoxCanvas)
    {
        UserNameInputBoxCanvas.transform.GetChild(2).gameObject.SetActive(true);
        UserNameInputBoxCanvas.transform.GetChild(0).gameObject.SetActive(false);
        UserNameInputBoxCanvas.transform.GetChild(1).gameObject.SetActive(false);
    }
    /* Method Name: enteredCreateName(GameObject textObj)
     * Summary: Checks if the username is taken, too long, or okay. 
     * @param textObj: The input field of the text. 
     * @return N/A
     * Special Effects: Display worning signs or not.
     */
    public void enteredCreateName(GameObject textObj) {
        string name = textObj.GetComponent<TMPro.TextMeshProUGUI>().text;
        if (string.IsNullOrEmpty(name) || name.Length == 1)
        {
            return;
        }
        else if (!seeIfEnteredCreatedPassword(GameObject.Find("PasswordText"))) {
            warningNote = "Please enter a password!";
            StartCoroutine(displayWarning());
        }
        else {
            name = name.Substring(0, name.Length - 1);
            if (name.Length > 17)
            {
                warningNote = "Username too long!";
                StartCoroutine(displayWarning());
            }
            else
            {
                bool successCreate = accManager.createAccount(name);
                if (!successCreate)
                {
                    warningNote = "Username already taken! Choose another username";
                    StartCoroutine(displayWarning());
                }
                else
                {
                    warningNote = "Account Created Successfully!";
                    StartCoroutine(displayWarning());
                }
            }
        }  
    }
    /* Method Name: enterCreatedPassword(GameObject textObj)
     * Summary: Checks if the password is actually entered. 
     * @param textObj: The input field of the text. 
     * @return N/A
     * Special Effects: Set the password of the account (or not).
     */
    public void enterCreatedPassword(GameObject textObj) {
        string pwd = textObj.GetComponent<TMPro.TextMeshProUGUI>().text;
        if (string.IsNullOrEmpty(pwd) || pwd.Length == 1)
        {
            return;
        }
        else {
            pwd = pwd.Substring(0, pwd.Length - 1);
            if (!(accManager.activeAccount == null))
            {
                accManager.activeAccount.password = pwd;
                accManager.activeAccount.saveAccount();
            }
        }
    }
    /* Method Name: seeIfEnteredCreatedPassword(GameObject textObj)
     * Summary: Checks if the password is actually entered. 
     * @param textObj: The input field of the text. 
     * @return true if there is a password entered.
     * Special Effects: N/A
     */
    public bool seeIfEnteredCreatedPassword(GameObject textObj) {
        string pwd = textObj.GetComponent<TMPro.TextMeshProUGUI>().text;
        if (string.IsNullOrEmpty(pwd) || pwd.Length == 1)
        {
            return false;
        }
        return true;
    }
    /* Method Name: resetInputField(GameObject inputField)
     * Summary: Resets the input field. 
     * @param inputField: The input field of the text. 
     * @return N/A
     * Special Effects: Resets the input field. 
     */
    public void resetInputField(GameObject inputField) {
        TMP_InputField mainInputField;
        mainInputField=inputField.GetComponent<TMP_InputField>();
        mainInputField.text = "";
    }
    /* Method Name: displayWarning()
     * Summary: Display warnings based on the coditions. When the sign in is successful it deactivates the login panel.
     * @param N/A
     * @return N/A
     * Special Effects: Display warnings.
     */
    IEnumerator displayWarning() {
        GameObject tempObj = mainMenuManager.UserNameInputBoxCanvas.transform.GetChild(3).gameObject;
        tempObj.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text= warningNote;
        tempObj.SetActive(true);
        for (int i = 0; i < mainMenuManager.buttonsToDisableOnWarning.Length; i++)
        {
            mainMenuManager.buttonsToDisableOnWarning[i].GetComponent<Button>().interactable = false;

        }
        yield return new WaitForSeconds(1f);
        tempObj.SetActive(false);
        
        if (warningNote == "Sign in Successfully!")
        {
            mainMenuManager.UserNameInputBoxCanvas.SetActive(false);
            GameObject.Find("MainPageCanvas").transform.GetChild(6).gameObject.SetActive(true);
        }
        else if (warningNote == "Account Created Successfully!") {
            mainMenuManager.UserNameInputBoxCanvas.transform.GetChild(2).gameObject.SetActive(false);
            mainMenuManager.UserNameInputBoxCanvas.transform.GetChild(0).gameObject.SetActive(true);
            mainMenuManager.UserNameInputBoxCanvas.transform.GetChild(1).gameObject.SetActive(false);
            mainMenuManager.UserNameInputBoxCanvas.transform.GetChild(4).gameObject.SetActive(true);
        }
        for (int i = 0; i < mainMenuManager.buttonsToDisableOnWarning.Length; i++)
        {
            mainMenuManager.buttonsToDisableOnWarning[i].GetComponent<Button>().interactable = true;
        }
        warningNote = "";
    }
    public IEnumerator displayWarning(bool auto)
    {
        GameObject tempObj = mainMenuManager.UserNameInputBoxCanvas.transform.GetChild(3).gameObject;
        tempObj.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = warningNote;
        tempObj.SetActive(true);
        for (int i = 0; i < mainMenuManager.buttonsToDisableOnWarning.Length; i++)
        {
            mainMenuManager.buttonsToDisableOnWarning[i].GetComponent<Button>().interactable = false;

        }
        yield return new WaitForSeconds(1f);
        tempObj.SetActive(false);

        if (true)
        {
            mainMenuManager.UserNameInputBoxCanvas.SetActive(false);
            GameObject.Find("MainPageCanvas").transform.GetChild(6).gameObject.SetActive(true);
        }
        for (int i = 0; i < mainMenuManager.buttonsToDisableOnWarning.Length; i++)
        {
            mainMenuManager.buttonsToDisableOnWarning[i].GetComponent<Button>().interactable = true;
        }
        warningNote = "";
        GameObject.Find("SceneManager").GetComponent<ButtonDetection>().executePlayButton();
    }
}
