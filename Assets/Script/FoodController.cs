using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    private GameState gameState;

    private Vector3 originalPosition;
    private Vector3 shopPosition;
    private Vector3 nullPosition = new Vector3(0f, 0f, -10000f);
    private Vector3 targetPosition;


    private Rigidbody2D rigidBody;
    private float spawnPositionY;

    protected Grid groundMap;
    protected float localScale;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = nullPosition;
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        originalPosition = transform.position;
        shopPosition = GameObject.Find("FoodImage").gameObject.transform.position;
        shopPosition = new Vector3(shopPosition.x, shopPosition.y, transform.position.z);
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        spawnPositionY = transform.localPosition.y;
        gameObject.SetActive(false);
        groundMap = GameObject.Find("GroundGrid").GetComponent<Grid>();
        localScale = groundMap.transform.localScale.x * groundMap.cellSize.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf && transform.localPosition.y < spawnPositionY - localScale / 3)
        {
            rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if(targetPosition != nullPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 30f * Time.deltaTime);
        }
    }
    private void OnMouseDown() {
        // gameObject.SetActive(false);
        //transform.position = originalPosition;
        targetPosition = shopPosition;
        //gameState.food += 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "FoodImage")
        {
            gameObject.SetActive(false);
            transform.position = originalPosition;
            gameState.food += 1;
            targetPosition = nullPosition;
        }
    }



    public void bounceFood()
    {
        spawnPositionY = transform.localPosition.y;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        rigidBody.velocity = new Vector2(localScale * 0.005f, localScale * 0.02f);
    }
}
