using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAntController : AntController
{
    public override void Awake()
    {
        base.Awake();
        health = 4;
        damage = 0;
        foodCost = 4;
        antName = "Wall";
    }
}
