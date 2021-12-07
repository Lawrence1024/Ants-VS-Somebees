using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyguardAntController : ContainerAntController
{
    public override void Awake()
    {
        base.Awake();
        health = 2;
        damage = 0;
        foodCost = 4;
        antName = "BGuard";
    }
}
