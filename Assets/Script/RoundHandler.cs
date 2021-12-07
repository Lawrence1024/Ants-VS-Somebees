using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundHandler : MonoBehaviour
{
    private float newTime;
    private float oldTime = 0f;
    private float roundTime = 1f;
    public GameState gameState;
    public BeeSpawner beeSpawner;
    public BeeManager beeManager;
    public int difficulty = 1;
    private int totalRounds = 100;
    private int roundCounter;
    private int spawnCounter = 5;
    private int spawnTimeMax = 10;
    private int spawnTimeMin = 3;
    private int spawnAmountMax = 2;
    private int spawnAmountMin = 1;
    // Start is called before the first frame update
    void Start()
    {
        setDifficultyStats();
        roundCounter = totalRounds;
        spawnCounter = Random.Range(spawnTimeMin, spawnTimeMax + 1);
    }

    private void setDifficultyStats()
    {
        if (difficulty == 1)
        {
            totalRounds = 20;
            spawnTimeMax = 10;
            spawnTimeMin = 8;
            spawnAmountMax = 1;
            spawnAmountMin = 1;
        }else if (difficulty == 2)
        {
            totalRounds = 40;
            spawnTimeMax = 10;
            spawnTimeMin = 8;
            spawnAmountMax = 1;
            spawnAmountMin = 1;
        }
        else if (difficulty == 3)
        {
            totalRounds = 60;
            spawnTimeMax = 10;
            spawnTimeMin = 7;
            spawnAmountMax = 1;
            spawnAmountMin = 1;
        }
        else if (difficulty == 4)
        {
            totalRounds = 80;
            spawnTimeMax = 10;
            spawnTimeMin = 6;
            spawnAmountMax = 1;
            spawnAmountMin = 1;
        }
        else if (difficulty == 5)
        {
            totalRounds = 110;
            spawnTimeMax = 10;
            spawnTimeMin = 5;
            spawnAmountMax = 1;
            spawnAmountMin = 1;
        }
        else if (difficulty == 6)
        {
            totalRounds = 140;
            spawnTimeMax = 10;
            spawnTimeMin = 4;
            spawnAmountMax = 1;
            spawnAmountMin = 1;
        }
        else if (difficulty == 7)
        {
            totalRounds = 170;
            spawnTimeMax = 10;
            spawnTimeMin = 4;
            spawnAmountMax = 2;
            spawnAmountMin = 1;
        }
        else if (difficulty == 8)
        {
            totalRounds = 200;
            spawnTimeMax = 10;
            spawnTimeMin = 4;
            spawnAmountMax = 2;
            spawnAmountMin = 1;
        }
        else if (difficulty == 9)
        {
            totalRounds = 230;
            spawnTimeMax = 7;
            spawnTimeMin = 4;
            spawnAmountMax = 3;
            spawnAmountMin = 1;
        }
        else if (difficulty == 10)
        {
            totalRounds = 270;
            spawnTimeMax = 6;
            spawnTimeMin = 4;
            spawnAmountMax = 4;
            spawnAmountMin = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        newTime = Time.time;
        if(newTime - oldTime > roundTime)
        {
            nextRound();
        }
    }
    private void nextRound()
    {
        oldTime = Time.time;
        roundCounter--;
        if (roundCounter <= 0 && beeManager.allBees.Count == 0)
        {
            gameState.endGame(true, difficulty);
        }
        spawnCounter--;
        if (spawnCounter == 0 && roundCounter > 0)
        {
            int beeCountBase = Random.Range(spawnAmountMin, spawnAmountMax + 1);
            int spawnTimeBase = Random.Range(spawnTimeMin, spawnTimeMax + 1);
            int multiplier = totalRounds / roundCounter / 6 * difficulty;
            if (multiplier < 1)
            {
                multiplier = 1;
            }
            beeSpawner.spawnBees(beeCountBase * multiplier);
            spawnCounter = spawnTimeBase / multiplier * multiplier;
        }
    }
}
