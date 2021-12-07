//FileName: ShowScoreBoardData.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: ShowScoreBoardData contains the functions to display rank on the scoreboard.  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowScoreBoardData : MonoBehaviour
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
    /* Method Name: getAccountsPoints(List<int> levelArr)
     * Summary: Sorts the rank of the users using their points. Display their ranks. If the active account's rank is in the top 5
     *          a celebratory message is displayed. 
     * @param levelArr: The list that contains information on which level the user is current on.
     * @return N/A
     * Special Effects: The ranks are displayed. 
     */
    public void getAccountsPoints(List<int> levelArr)
    {
        celebratoryMsgCanvas.SetActive(false);
        int levelValue = levelArr[0] * 3 + levelArr[1] - 4;
        accountsManager = GameObject.Find("AccountsManager").GetComponent<AccountsManager>();
        List<Account> highToLow = new List<Account>();
        for (int i = 0; i < accountsManager.accounts.Count; i++)
        {
            Account acc = accountsManager.accounts[i];
            if ((i == 0 && acc.pointsList[levelValue] != -1)||(highToLow.Count==0 && acc.pointsList[levelValue] != -1))
            {
                highToLow.Add(acc);
            }
            else if (acc.pointsList[levelValue]!=-1)
            {
                for(int j = 0; j < highToLow.Count; j++)
                {
                    if (acc.pointsList[levelValue] > highToLow[j].pointsList[levelValue])
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
        if (highToLow.Count < rankTexts.Length)
        {
            for (int i = 0; i < highToLow.Count; i++)
            {
                string text;
                text = (i + 1) + ". " + highToLow[i].userName;
                int tempTextLength = text.Length;
                rankTexts[i].GetComponent<TMPro.TextMeshProUGUI>().text = text;
                rankTexts[i].transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = highToLow[i].pointsList[levelValue].ToString();
            }
        }
        else
        {
            for (int i = 0; i < rankTexts.Length; i++)
            {
                string text;
                text = (i + 1) + ". " + highToLow[i].userName;
                int tempTextLength = text.Length;
                rankTexts[i].GetComponent<TMPro.TextMeshProUGUI>().text = text;
                rankTexts[i].transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = (highToLow[i].pointsList[levelValue]).ToString();
            }
        }
        for (int i = 0; i < highToLow.Count; i++) {

            if (highToLow[i].userName == accountsManager.activeAccount.userName) {
                GameObject.Find("Rank").GetComponent<TMPro.TextMeshProUGUI>().text = "Highest Rank: " + (i + 1);
                if (highToLow[i].pointsList[levelValue]<= int.Parse(GameObject.Find("PointsValue").GetComponent<TMPro.TextMeshProUGUI>().text)&&i<5) {
                    celebratoryMsgCanvas.transform.GetChild(3).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = (i+1).ToString();
                    celebratoryMsgCanvas.SetActive(true);
                }
                break;
            }
            else{
                if (i>30) {
                    GameObject.Find("Rank").GetComponent<TMPro.TextMeshProUGUI>().text = "Highest Rank: No Rank";
                }
            }
        }
    }
}
