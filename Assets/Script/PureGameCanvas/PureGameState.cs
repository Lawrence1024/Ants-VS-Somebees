using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PureGameState : GameState
{
    // Start is called before the first frame update
    private void Awake()
    {
        food = 10f;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void endGame(bool win, int difficulty)
    {
        gameEnd = true;
    }
}
