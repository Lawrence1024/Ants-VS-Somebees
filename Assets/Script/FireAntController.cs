using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAntController : AntController
{
    public override void Awake()
    {
        base.Awake();
        health = 3;
        damage = 3;
        foodCost = 5;
        antName = "Fire";
    }
    public override void reduceHealth(float amount)
    {
        float returnDamage = amount;
        if(place.entrance != null)
        {
            if(amount >= health)
            {
                returnDamage += damage;
            }
            List<BeeController> cloneBees = new List<BeeController>(place.entrance.bees);
            foreach(var b in cloneBees)
            {
                b.reduceHealth(returnDamage);
            }
        }
        base.reduceHealth(amount);
    }
}
