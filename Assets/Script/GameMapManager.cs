using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameMapManager : MonoBehaviour
{
    [SerializeField]
    public Tilemap map;

    [SerializeField]
    private List<TileData> tileDatas;

    private Dictionary<TileBase, TileData> dataFromTiles;
    public List<int> floorCoard = new List<int>();
    public Dictionary<Vector3Int, TileData> tileInstances;
    public AntManager antManager;
    public BeeManager beeManager;
    public InsectController insectController;
    public AntShop antShop;
    

    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        tileInstances = new Dictionary<Vector3Int, TileData>();
        foreach (var tileData in tileDatas)
        {
            foreach(var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
            if(tileData.name == "Ground")
            {
                createGroundInstances(tileData);
            }else if(tileData.name == "BeeHive")
            {
                createHive(tileData, floorCoard[2], true, false);
            }
            else if (tileData.name == "AntHive")
            {
                createHive(tileData, floorCoard[0], false, true);
            }
        }
    }

    private void Update()
    {
        checkClick();
    }
    private void createHive(TileData tileData, int x, bool isBeeHive, bool isAntHive)
    {
        for (var i = floorCoard[1]; i <= floorCoard[3]; i++)
        {
            tileInstances[new Vector3Int(x, i, 0)].isBeeHive = isBeeHive;
            tileInstances[new Vector3Int(x, i, 0)].isAntHive = isAntHive;
        }
    }
    private void createGroundInstances(TileData tileData)
    {
        for(var i=floorCoard[1]; i<= floorCoard[3]; i++)
        {
            for(var j=floorCoard[0]; j<=floorCoard[2]; j++)
            {
                createPlaceInstances(tileData, false, false, j, i);
            }
        }
    }

    private void createPlaceInstances(TileData tileData, bool isBeeHive, bool isAntHive, int x, int y)
    {
        tileInstances.Add(new Vector3Int(x, y, 0), ScriptableObject.CreateInstance<TileData>());
        TileData currentTile = tileInstances[new Vector3Int(x, y, 0)];
        currentTile.cord = new int[2] { x, y };
        currentTile.isBeeHive = isBeeHive;
        currentTile.isAntHive = isAntHive;
        if (x == floorCoard[0])
        {
            currentTile.exit = null;
        }
        else if (x == floorCoard[2])
        {
            currentTile.entrance = null;
            currentTile.exit = tileInstances[new Vector3Int(x - 1, y, 0)];
            tileInstances[new Vector3Int(x - 1, y, 0)].entrance = currentTile;
        }
        else
        {
            currentTile.exit = tileInstances[new Vector3Int(x - 1, y, 0)];
            tileInstances[new Vector3Int(x - 1, y, 0)].entrance = currentTile;
        }
    }   
        
    private void printCord(TileData t)
    {
        if(t == null)
        {
            Debug.Log(null);
        }
        else
        {
            Debug.Log(t.cord[0] + " " + t.cord[1]);
        }
    }

    private void checkClick()
    {
        //if (Input.GetMouseButtonUp(0) && antShop.selectedCard)
        //{
        //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    Vector3Int gridPosition = map.WorldToCell(mousePosition);
        //    if (tileInstances.ContainsKey(gridPosition) && !(tileInstances[gridPosition].isAntHive || tileInstances[gridPosition].isBeeHive))
        //    {
        //        GameObject.Find("AntSpawner").GetComponent<AntSpawner>().spawnAnt(gridPosition);
        //    }
        //}
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    Vector3Int gridPosition = map.WorldToCell(mousePosition);
        //
        //    TileBase clickedTile = map.GetTile(gridPosition);
        //
        //
        //    //GameObject.Find("AntSpawner").GetComponent<AntSpawner>().spawnAnt(gridPosition);
        //    
        //}
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    GameObject.Find("BeeSpawner").GetComponent<BeeSpawner>().spawnBees(1);
        //}
    }

    public float getTileWalkingSpeed(Vector2 worldPosition)
    {
        Vector3Int gridPosition = map.WorldToCell(worldPosition);

        TileBase tile = map.GetTile(gridPosition);

        if (tile == null)
            return 1f;

        float walkingSpeed = dataFromTiles[tile].walkingSpeed;
        return walkingSpeed;

    }
}
