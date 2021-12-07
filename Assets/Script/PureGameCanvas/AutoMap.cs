using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMap : MonoBehaviour
{
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("MapManager").GetComponent<levelButtonDetection>().changeLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
