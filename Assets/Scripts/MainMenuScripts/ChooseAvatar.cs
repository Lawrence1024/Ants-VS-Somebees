//FileName: ChooseAvatar.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: ChooseAvatar contains the functions that run when an avatar is chosen.  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseAvatar : MonoBehaviour
{
    private Vector4 avaColor;
    public AccountsManager accManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    /* Method Name: chooseColor(GameObject userColor)
     * Summary: When the avatar button is pressed, it set the account's avatar to a specific color.
     * @param userColor: Contain the information on which color was chose.
     * @return N/A
     * Special Effects: The account's avatar is set. 
     */
    public void chooseColor(GameObject userColor) {
        Scene currentScene = SceneManager.GetActiveScene();
        if (accManager.activeAccount == null)
        {
            return;
        }
        if (userColor.name == "Red") {
            accManager.activeAccount.avatarColor = new Vector4(1, 0.24f, 0.24f, 1);
        } else if (userColor.name=="Orange") {
            accManager.activeAccount.avatarColor = new Vector4(1, 0.35f, 0, 1);
        }
        else if (userColor.name == "Yellow")
        {
            accManager.activeAccount.avatarColor = new Vector4(1, 1, 0, 1);
        }
        else if (userColor.name == "Green")
        {
            accManager.activeAccount.avatarColor = new Vector4(0.29f, 1, 0, 1);

        }
        else if (userColor.name == "Blue")
        {
            accManager.activeAccount.avatarColor = new Vector4(0, 0.53f, 1, 1);

        }
        else if (userColor.name == "Purple")
        {
            accManager.activeAccount.avatarColor = new Vector4(1, 0.24f, 0.67f, 1);

        }
        else if (userColor.name == "White")
        {
            accManager.activeAccount.avatarColor = new Vector4(1, 1, 1, 1);
        }
        accManager.activeAccount.saveAccount();
        if (currentScene.name=="MainMenu") {
            accManager.activeAccount = null;
        }
    }
    /* Method Name: inactiveAvatarBoard(GameObject avatarBoard)
     * Summary: When the avatar button is pressed, it set the parent of the avatar button to deactivated. 
     * @param avatarBoard: The parent of the avatar buttons.
     * @return N/A
     * Special Effects: The avatar board is deactivated.
     */
    public void inactiveAvatarBoard(GameObject avatarBoard) {
        avatarBoard.SetActive(false);
    }
}
