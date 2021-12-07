using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryAntController : AntController
{
    public int chewCountDown = 0;
    public int chewDuration = 3;
    public override void Awake()
    {
        base.Awake();
        health = 1;
        damage = 0;
        foodCost = 4;
        antName = "Hungry";
    }
    public override void nextRound()
    {
        base.nextRound();
        if (place.entrance != null)
        {
            if (chewCountDown > 0)
            {
                chewCountDown -= 1;
            }else if(place.entrance.bees.Count > 0)
            {
                BeeController beeToEat = place.entrance.bees[0];
                beeToEat.reduceHealth(beeToEat.health);
                chewCountDown = chewDuration;
            }
        }
    }
}
