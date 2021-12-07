using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerAntController : AntController
{
    public PeaSpawner peaSpawner;
    public int maxRange;
    public int minRange;

    public override void Awake()
    {
        health = 1;
        damage = 1;
        foodCost = 3;
        antName = "Thrower";
        maxRange = 100;
        minRange = 0;
    }

    public override void Start()
    {
        base.Start();
        peaSpawner = transform.parent.Find("PeaSpawner").GetComponent<PeaSpawner>();
        
    }

    public override void nextRound()
    {
        base.nextRound();
        if (beeInRange())
        {
            peaSpawner.spawnPeas(1);
        }
    }
    public virtual bool beeInRange()
    {

        TileData inspected_place = place;
        int distance_counter = 0;
        while (!inspected_place.isBeeHive && !inspected_place.isAntHive)
        {
            if (minRange <= distance_counter && distance_counter <= maxRange && inspected_place.bees.Count > 0)
            {
                return true;
            }
            else
            {
                distance_counter += 1;
                inspected_place = inspected_place.entrance;
            }
        }
        return false;
    }
}
