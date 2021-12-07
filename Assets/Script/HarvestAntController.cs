using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestAntController : AntController
{
    public int foodCountDown = 3;
    public GameObject foodObj;

    public override void Awake()
    {
        base.Awake();
        health = 1;
        damage = 0;
        foodCost = 2;
        antName = "Harvester";
    }

    public override void Start()
    {
        base.Start();
        foodObj.transform.position = transform.position;
        
    }
    public override void nextRound()
    {
        base.nextRound();
        if (!foodObj.activeSelf)
        {
            foodCountDown--;
            if (foodCountDown == 0)
            {
                foodCountDown = 3;
                foodObj.SetActive(true);
                foodObj.GetComponent<FoodController>().bounceFood();
            }
        }
    }
}
