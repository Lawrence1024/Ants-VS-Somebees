//FileName: DontDestroy.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: DontDestroy preserves the gameobject that the script is attatched to. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance;
    /* Method Name: Start()
     * Summary: Check if the object exist or not. If so, destroy the duplicate object.
     * @param N/A
     * @return N/A
     * Special Effects: Destroy the duplicate object if needed.
     */
    void Start()
    {
        DontDestroyOnLoad(this);
        if (instance == null) {
            instance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
