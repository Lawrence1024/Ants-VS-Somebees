using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]

public class TileData : ScriptableObject
{
    public TileBase[] tiles;
    public float walkingSpeed, poison;
    public bool isBeeHive;
    public bool isAntHive;
    public AntController ant = null;
    public List<BeeController> bees = new List<BeeController>();
    public TileData entrance = null;
    public TileData exit = null;
    public int[] cord;


}
