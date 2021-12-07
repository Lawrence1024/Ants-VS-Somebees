using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAntController : ContainerAntController
{
    public override void Awake()
    {
        base.Awake();
        health = 2;
        damage = 1;
        foodCost = 6;
        antName = "Tank";
    }
    public override void nextRound()
    {
        base.nextRound();
        if (place.entrance != null)
        {
            foreach (var bee in new List<BeeController>(place.entrance.bees))
            {
                bee.reduceHealth(damage);
            }
        }
    }
}
