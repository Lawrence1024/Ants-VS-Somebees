//FileName: ChangeAvatar.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: ChangeAvatar contains functions to change and update avatar 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChangeAvatar : MonoBehaviour

{
    private Account activeAccount;
    /* Method Name: Start()
     * Summary: Get the game object "AccountsManager"'s activeAccount (the current signed in account).
     * @param N/A
     * @return N/A
     * Special Effects: The account is saved to the variable activeAccount.
     */
    void Start()
    {
        activeAccount = GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    /* Method Name: chnageAvatarColor()
     * Summary: Activate the choosing avatar panel in the game object "AccountsManager" and deactivate other game object.
     * @param N/A
     * @return N/A
     * Special Effects: This instance of account is saved.
     */
    public void changeAvatarColor() {
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.SetActive(false);
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.SetActive(true);
    }
    /* Method Name: updateAvatarInScene()
     * Summary: When the button (avatar color option) is pressed, this function sets the avatar's color to the chosen color on the map.
     * @param N/A
     * @return N/A
     * Special Effects: The color of the avatar changes on the map.
     */
    public void updateAvatarInScene() {
        Scene currentScene = SceneManager.GetActiveScene();
        activeAccount = GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount;
        if (currentScene.name == "Map") {
            GameObject.Find("AvatarColor").GetComponent<Image>().color = activeAccount.avatarColor;
        }
    }
}
