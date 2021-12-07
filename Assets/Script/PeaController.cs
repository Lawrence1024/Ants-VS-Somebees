using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public AntController antController;
    public bool didHit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 destination = new Vector3(transform.position.x + 20, transform.position.y, 0f);
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("Bees") || col.collider.gameObject.layer == LayerMask.NameToLayer("Perimeter"))
        {
            Destroy(gameObject);
        }
    }
}
