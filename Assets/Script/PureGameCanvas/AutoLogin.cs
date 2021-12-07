using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLogin : MonoBehaviour
{
    static bool firstTime = true;
    private void Awake()
    {
        if (firstTime)
        {
            firstTime = false;
            SceneManager.LoadScene("MainMenu");
        }
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
