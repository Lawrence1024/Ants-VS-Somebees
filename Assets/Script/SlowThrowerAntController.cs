using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowThrowerAntController : ThrowerAntController
{
    public override void Awake()
    {
        base.Awake();
        health = 1;
        foodCost = 4;
        antName = "Slow";
    }
}
