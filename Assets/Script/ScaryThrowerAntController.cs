using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryThrowerAntController : ThrowerAntController
{
    public override void Awake()
    {
        base.Awake();
        health = 1;
        foodCost = 6;
        antName = "Scary";
    }
}
