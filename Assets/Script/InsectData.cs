using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InsectData
{
    public float damage = 1f;
    public bool is_waterproof = false;
    protected float health = 10f;
    public TileData place = null;
    public int xPos;
    public int yPos;
    public Grid groundMap;
    public float localScale;
    public float convertingScale;
    public float newTime;
    public float oldTime = 0f;
    public float roundTime = 1f;
    public Animator animator;

    public InsectData(Grid groundMap)
    {
        this.groundMap = groundMap;
    }
}
