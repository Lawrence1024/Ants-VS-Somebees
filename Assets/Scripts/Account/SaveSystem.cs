//FileName: SaveSystem.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: SaveSystem will store/load player account information onto the computer (so user can keep their progress after quit).
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    /* Method Name: saveAccount(Account account)
     * Summary: Store an account onto the computer so users can save their progress and records.
     * @param account: The account that user wants to save.
     * @return N/A
     * Special Effects: Account information is stored in a secure folder on the computer. 
     */
    public static void saveAccount(Account account)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string tempName = account.userName;
        string path = Application.persistentDataPath + "/" + tempName + ".account";
        FileStream stream = new FileStream(path, FileMode.Create);
        AccountData data = new AccountData(account);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    /* Method Name: loadAccount(Account account)
     * Summary: Look into the secure folder and attempt to find the user data.
     * @param account: The account that user wants to load.
     * @return an instance of AccountData which contains the user information requested. Return null if file not found. 
     * Special Effects: N/A
     */
    public static AccountData loadAccount(Account account)
    {
        string tempName = account.userName;
        string path = Application.persistentDataPath + "/"+tempName+".account";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            AccountData data = formatter.Deserialize(stream) as AccountData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
}
