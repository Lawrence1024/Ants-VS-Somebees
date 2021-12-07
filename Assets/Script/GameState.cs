using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public float food;
    public LevelManager levelManager;
    public bool gameEnd = false;
    public static bool hasQueen;
    private Account activeAccount;
    // Start is called before the first frame update
    private void Awake()
    {
        init();
    }
    void Start()
    {
        activeAccount = GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void init()
    {
        food = 10f;
        hasQueen = false;
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }
    public virtual void endGame(bool win, int difficulty)
    {
        if (win)
        {
            activeAccount.starsList[difficulty - 1] = 3;
            activeAccount.saveAccount();
            if(difficulty == 10)
            {
                StartCoroutine(levelManager.loadWarning("You Win!", 3, true));
            }
            else
            {
                StartCoroutine(levelManager.loadWarning("You Win!\nBack To Map In", 3));
            }
        }
        else
        {
            StartCoroutine(levelManager.loadWarning("Oh no! You Lost!\nBack To Map In", 3));
        }
        gameEnd = true;
    }
}
