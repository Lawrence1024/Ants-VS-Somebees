using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenAntController : ThrowerAntController
{
    public override void Awake()
    {
        base.Awake();
        health = 1;
        foodCost = 7;
        antName = "Queen";
    }
    public override void nextRound()
    {
        base.nextRound();
        TileData current_place = place.exit;
        while(current_place != null)
        {
            AntController current_ant = current_place.ant;
            if(current_ant != null)
            {
                if (current_ant.isContainer)
                {
                    if (!current_ant.buffed)
                    {
                        current_ant.buff();
                    }
                    if(current_ant.antContained && (!current_ant.antContained.buffed))
                    {
                        current_ant.antContained.buff();
                    }
                }
                else if(!current_ant.buffed)
                {
                    current_ant.buff();
                }
            }
            current_place = current_place.exit;
        }
    }
    public override void reduceHealth(float amount)
    {
        if(amount >= health)
        {
            gameState.endGame(false, -1);
        }
        base.reduceHealth(amount);
    }
}
