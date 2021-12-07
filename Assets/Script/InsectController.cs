using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectController : MonoBehaviour
{
    public float damage;
    protected bool is_waterproof = false;
    public float health;
    protected TileData place = null;
    protected int xPos;
    protected int yPos;
    protected Grid groundMap;
    protected float localScale;
    protected float convertingScale;
    protected float newTime;
    protected float oldTime = 0f;
    protected float roundTime = 1f;
    [SerializeField]
    protected Animator animator;
    protected GameState gameState;
    

    public virtual void Awake()
    {
        health = 1f;
        damage = 1f;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        groundMap = GameObject.Find("GroundGrid").GetComponent<Grid>();
        localScale = groundMap.transform.localScale.x * groundMap.cellSize.x;
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        
        try
        {
            animator = gameObject.GetComponent<Animator>();
        }
        catch
        {
            Debug.Log("Called");
            animator = null;
        }
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    //Setter & Getter
    public void setHealth(float amount){ health = amount; }
    public void setDamage(float amount){ damage = amount; }
    public void setXPos(int x) { xPos = x; }
    public void setYPos(int y) { yPos = y; }
    public void setLocalScale(float scale) { localScale = scale; }
    
    public float getHealth(){ return health; }
    public float getDamage(){ return damage; }
    public int getXPos() { return xPos; }
    public int getYPos() { return yPos; }
    public float getLocalScale() { return localScale; }

    public void timeTrack()
    {
        newTime = Time.time;
        if (newTime - oldTime > roundTime && !gameState.gameEnd)
        {
            nextRound();
        }
    }

    public virtual void nextRound()
    {
        oldTime = Time.time;
    }
    public virtual bool addTo(TileData tile)
    {
        place = tile;
        return true;
    }
    public virtual void deathCallback()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
    public virtual void reduceHealth(float amount)
    {
        health -= amount;
        animator.SetTrigger("GetHit");
        if(health <= 0)
        {
            deathCallback();
        }
    }
    public virtual void removeFrom(TileData tile)
    {
        place = null;
    }
}
