using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject antPrefab;
    [SerializeField]
    private GameObject antManager;

    public GameMapManager mapManager;
    private Dictionary<Vector3Int, TileData> tileInstances;
    public GameState gameState;
    void Start()
    {
        tileInstances = mapManager.tileInstances;
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        //init();
    }

    private void init()
    {
        List<GameObject> allAnt = antManager.GetComponent<AntManager>().allAnts;
        foreach(var a in allAnt)
        {
            int x = -3;
            int y = 0;
            AntController antController = a.GetComponent<AntController>();
            antController.addTo(tileInstances[new Vector3Int(x, y, 0)]);
            antController.setYPos(y);
            antController.setXPos(x);
            a.GetComponent<RectTransform>().localPosition = new Vector3(antController.getXPos() * antController.getLocalScale() + antController.getLocalScale() / 2f, antController.getYPos() * antController.getLocalScale() + antController.getLocalScale() / 2f, 0f);
            antController.movePoint.position = a.transform.position;
        }
    }

    public void spawnAnt(CardController selectedCard, Vector3Int pos)
    {
        GameObject tempAnt = GameObject.Instantiate(selectedCard.antPrefab, new Vector3(0f, 0f, 0f), transform.rotation);
        AntController antController = tempAnt.transform.Find("AntObj").GetComponent<AntController>();
        bool success = antController.addTo(tileInstances[pos]);
        if (success)
        {
            if(antController.antName == "Queen")
            {
                GameState.hasQueen = true;
            }
            gameState.food -= selectedCard.foodCost;
            List<GameObject> allAnts = antManager.GetComponent<AntManager>().allAnts;
            tempAnt.transform.SetParent(antManager.transform);
            tempAnt.transform.localScale = new Vector3(1f, 1f, 1f);
            tempAnt.transform.localPosition = new Vector3(0f, 0f, 0f);
            allAnts.Add(tempAnt.transform.Find("AntObj").gameObject);
            antController.setYPos(pos.y);
            antController.setXPos(pos.x);
        }
        else
        {
            Destroy(tempAnt);
        }
        
    }

    private void Update()
    {

    }
}
