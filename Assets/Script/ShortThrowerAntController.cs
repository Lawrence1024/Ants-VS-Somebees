using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortThrowerAntController : ThrowerAntController
{
    public override void Awake()
    {
        base.Awake();
        health = 1;
        damage = 2;
        foodCost = 2;
        antName = "Short";
        maxRange = 3;
    }
}
