//FileName: LeaderBoardDisplay.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: LeaderBoardDisplay contains the functions to display rank on the leaderboard.  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardDisplay : MonoBehaviour
{
    AccountsManager accountsManager;
    public GameObject[] rankTexts;
    public GameObject celebratoryMsgCanvas;
    /* Method Name: Start()
     * Summary: Get the game object "AccountsManager"'s script "AccountsManager" (a script attatched to the AccountsManager). 
     * @param N/A
     * @return N/A
     * Special Effects: The script is saved to the variable accountsManager.
     */
    void Start()
    {
        accountsManager = GameObject.Find("AccountsManager").GetComponent<AccountsManager>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    /* Method Name: getAccountsTotalStars()
     * Summary: Sorts the rank of the users using their star counts. Display their ranks. If the active account's rank is in the top 5
     *          a celebratory message is displayed. 
     * @param N/A
     * @return N/A
     * Special Effects: The ranks are displayed. 
     */
    public void getAccountsTotalStars() {
        celebratoryMsgCanvas.SetActive(false);
        accountsManager = GameObject.Find("AccountsManager").GetComponent<AccountsManager>();
        List<Account> highToLow = new List<Account>();
        for(int i = 0; i < accountsManager.accounts.Count; i++)
        {
            Account acc = accountsManager.accounts[i];
            if (i == 0)
            {
                highToLow.Add(acc);
            }
            else
            {
                for (int j = 0; j < highToLow.Count; j++)
                {
                    if (acc.getTotalStar() > highToLow[j].getTotalStar())
                    {
                        highToLow.Insert(j, acc);
                        break;
                    }
                    if (j == highToLow.Count - 1)
                    {
                        highToLow.Add(acc);
                        break;
                    }
                }
            }
        }
        for (int i = 0; i < highToLow.Count; i++) {
            string text;
            text = (i+1) + ". " + highToLow[i].userName;
            rankTexts[i].GetComponent<TMPro.TextMeshProUGUI>().text= text;
            rankTexts[i].transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text= highToLow[i].getTotalStar().ToString();
            if (highToLow[i].userName == accountsManager.activeAccount.userName&&i<5) {
                celebratoryMsgCanvas.transform.GetChild(3).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = (i + 1).ToString();
                celebratoryMsgCanvas.SetActive(true);
            }
        }
    }
}
