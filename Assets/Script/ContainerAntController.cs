using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerAntController : AntController
{
    public int foodCountDown = 3;
    public GameObject foodObj;

    public override void Awake()
    {
        base.Awake();
        health = 0;
        damage = 0;
        foodCost = 0;
        isContainer = true;
        antName = "ContainerAnt";
    }

    public override bool canContain(AntController other)
    {
        return !antContained && !other.isContainer;
    }
    public override void storeAnt(AntController other)
    {
        antContained = other;
    }
    public override void removeAnt(AntController other)
    {
        if (antContained != other)
        {
            Debug.Log(name + " does not contain " + other.name);
        }
        else
        {
            antContained = null;
        }
    }
    public override void removeFrom(TileData tile)
    {
        if(tile.ant == this)
        {
            tile.ant = tile.ant.antContained;
        }
        base.removeFrom(tile);
    }
}
