//FileName: BoxManager.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: BoxManager is a centralized control unit of all the boxes. It consist a list of all the boxes.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Tilemaps;
public class AntManager : MonoBehaviour
{
    public List<GameObject> allAnts;
    public GameMapManager mapManager;
    public Tilemap map;
    public Dictionary<Vector3Int, TileData> tileInstances;
    //LevelManager levelManager;
    // Start is called before the first frame update
    /* Method Name: Start()
     * Summary: Initialize the value of levelManager.
     * @param N/A
     * @return N/A
     * Special Effects: N/A
     */
    void Start()
    {
        tileInstances = mapManager.tileInstances;
        map = mapManager.map;
        //levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void placeAnt(CardController selectedCard, Vector3Int gridPosition)
    {
        GameObject.Find("AntSpawner").GetComponent<AntSpawner>().spawnAnt(selectedCard, gridPosition);
    }
}
