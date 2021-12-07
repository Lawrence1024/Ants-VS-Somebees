//FileName: Account.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: Account will record different information of the user account.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Account
{
    //fields
    public string userName;
    public string password;
    public int totalStar;
    public List<int> starsList;
    public List<int> potentialStarsList;
    public List<int> pointsList;
    public Vector4 avatarColor;
    public List<bool> tutorialProgress;
    public List<int> tutorialFeatures;
    public bool endSceneActivated;
    //constructor
    /* Constructor Name: Account(string name)
     * Summary: Given a username, create an empty account with default values.
     * @param name: The registered username.
     * @return the instance of the new created account.
     * Special Effects: N/A
     */
    public Account(string name)
    {
        userName = name;
        password = "lol";
        totalStar = 0;
        starsList = new List<int> { -1,-1,-1,-1,-1,-1,-1,-1,-1,-1 };
        potentialStarsList = new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };
        pointsList = new List<int> { -1,-1,-1,-1,-1,-1,-1,-1,-1,-1 };
        avatarColor = new Vector4(1f, 1f, 1f, 1f);
        tutorialProgress = new List<bool> { false, false };
        tutorialFeatures = new List<int> { 3, 3 };
        endSceneActivated = false;
    }
    //public method
    /* Method Name: saveAccount()
     * Summary: Call on the saveAccount method in the SaveSystem object to store player data onto computer.
     * @param N/A
     * @return N/A
     * Special Effects: This instance of account is saved.
     */
    public void saveAccount()
    {
        SaveSystem.saveAccount(this);
    }
    /* Method Name: loadAccount()
     * Summary: Call on the loadAccount method in the SaveSystem object and retreieve an instance of AccountData from previously saved files.
     *          Fill this instance of Account with the retrieved data.
     * @param N/A
     * @return N/A
     * Special Effects: This instance of Account have the updated player information fetched from previously saved files.
     */
    public void loadAccount()
    {
        AccountData data = SaveSystem.loadAccount(this);
        userName = data.userName;
        password = data.password;
        totalStar = data.totalStar;
        starsList = data.starsList;
        potentialStarsList = data.potentialStarsList;
        pointsList = data.pointsList;
        avatarColor = new Vector4(data.avatarColor[0], data.avatarColor[1], data.avatarColor[2], data.avatarColor[3]);
        tutorialProgress = data.tutorialProgress;
        tutorialFeatures = data.tutorialFeatures;
        endSceneActivated = data.endSceneActivated;
    }
    /* Method Name: getTotalStar()
     * Summary: With a list of stars, add up each number to get a total star count.
     * @param N/A
     * @return the total amount of stars the player currenlty has
     * Special Effects: N/A
     */
    public int getTotalStar()
    {
        int total = 0;
        foreach (int score in starsList)
        {
            if (score == -1)
            {
                break;
            }
            total += score;
        }
        return total;
    }
}
