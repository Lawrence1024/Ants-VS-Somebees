//FileName: AccountsManager.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: Account will record different information of the user account.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AccountsManager : MonoBehaviour
{
    public List<Account> accounts;
    private int activeIndex=-1;
    public Account activeAccount=null;
    // Start is called before the first frame update
    /* Method Name: Start()
     * Summary: Go through the secured folder and look for any prexisting user data. Only fetch files that is the type ".account".
     * @param N/A
     * @return N/A
     * Special Effects: A list of Account is created.
     */
    void Start()
    {
        accounts = new List<Account>();
        foreach (string file in System.IO.Directory.GetFiles(Application.persistentDataPath))
        {
            if(file.Length>8 && file.Substring(file.Length - 8, 8)==".account")
            {
                int nameLength = file.Length - Application.persistentDataPath.Length - 9;
                string accountName = file.Substring(Application.persistentDataPath.Length + 1, nameLength);
                Account tempAcc = new Account(accountName);
                accounts.Add(tempAcc);
                tempAcc.loadAccount();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /* Method Name: createAccount(string name)
     * Summary: Create a new account if the quired username is not already taken.
     * @param name: The username which the player is trying to register.
     * @return whether there is already a prexisitng account with the username. Return true if name is already taken, false if not.
     * Special Effects: N/A
     */
    public bool createAccount(string name)
    {
        bool find = checkIfAccountTaken(name);
        if (!find)
        {
            Account tempAcc = new Account(name);
            accounts.Add(tempAcc);
            tempAcc.saveAccount();
            activeAccount = tempAcc;
        }
        return !find;
    }
    /* Method Name: checkIfAccountTaken(string name)
     * Summary: Go through a list of accounts and check if any accounts have already used the desired username.
     * @param name: The username which the player is trying to register.
     * @return whether there is already a prexisitng account with the username. Return true if name is already taken, false if not.
     * Special Effects: N/A
     */
    public bool checkIfAccountTaken(string name)
    {
        for (int i = 0; i < accounts.Count; i++)
        {
            if (name.Equals(accounts[i].userName))
            {
                return true;
            }
        }
        return false;
    }
    /* Method Name: loadAccount(GameObject textArea)
     * Summary: Go through a list of accounts and load the account with a specific username.
     * @param textArea: A text area where we could read the user input to see which account they would like to load.
     * @return N/A
     * Special Effects: N/A
     */
    public void loadAccount(GameObject textArea)
    {
        Account account = null;
        
        string name = textArea.GetComponent<TMPro.TextMeshProUGUI>().text;
        
            bool find = checkIfAccountExist(name);
        if (find)
        {
            account = accounts[activeIndex];
            activeAccount = accounts[activeIndex];
        }
    }
    public void loadAccount()
    {
        activeAccount = accounts[0];
    }
    /* Method Name: confirmLogin(string password)
     * Summary: Each account has its own password. Check if the inputed password match the stored password.
     * @param password: A string of user inputed password
     * @return ture if password matches, false if not.
     * Special Effects: If the password is incorrect, the activeAccount will be set to null.
     */
    public bool confirmLogin(string password)
    {
        if (activeAccount!=null && activeAccount.password.Equals(password))
        {
            return true;
        }
        activeAccount = null;
        activeIndex = -1;
        return false;
    }
    /* Method Name: checkIfAccountExist(string name)
     * Summary: In the array of accounts, check if the account with the given name already exists.
     * @param name: A string of user inputed username
     * @return ture if account already exist, false if not.
     * Special Effects: N/A
     */
    public bool checkIfAccountExist(string name)
    {
        bool find = false;
        name = name.Substring(0,name.Length-1);
        for(int i = 0; i < accounts.Count; i++)
        {
            if (name.Equals(accounts[i].userName))
            {
                find = true;
                activeIndex = i;
            }
        }
        return find;
    }
    /* Method Name: accountGreaterThan(Account acc1, Account acc2)
     * Summary: Take in two accounts and see if the first account have more total stars than the second
     * @param acc1: An instance of account
     * @param acc2: An instance of account
     * @return ture if first acount have more stars than the second account
     * Special Effects: N/A
     */
    public bool accountGreaterThan(Account acc1, Account acc2)
    {
        return acc1.getTotalStar() > acc2.getTotalStar();
    }
}
