using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject beePrefab;
    [SerializeField]
    private GameObject beeManager;

    public GameMapManager mapManager;
    private Dictionary<Vector3Int, TileData> tileInstances;
    void Start()
    {
        tileInstances = mapManager.tileInstances;
        init();
    }

    private void init()
    {
        //GameObject tempBee = GameObject.Instantiate(beePrefab, new Vector3(0f, 0f, 0f), transform.rotation);
        List<GameObject> allBees = beeManager.GetComponent<BeeManager>().allBees;
        //tempBee.transform.SetParent(beeManager.transform);
        //tempBee.transform.localScale = new Vector3(1f, 1f, 1f);
        //tempBee.transform.localPosition = new Vector3(0f, 0f, 0f);
        //allBees.Add(tempBee.transform.Find("BeeObj").gameObject);
        foreach (var b in allBees)
        {
            int randomY = Random.Range(mapManager.floorCoard[1], mapManager.floorCoard[3] + 1);

            randomY = 0;

            BeeController beeController = b.GetComponent<BeeController>();
            beeController.addTo(tileInstances[new Vector3Int(mapManager.floorCoard[2], randomY, 0)]);
            beeController.setYPos(randomY);
            beeController.setXPos(mapManager.floorCoard[2]);
            b.GetComponent<RectTransform>().localPosition = new Vector3(beeController.getXPos() * beeController.getLocalScale() + beeController.getLocalScale() / 2f, beeController.getYPos() * beeController.getLocalScale() + beeController.getLocalScale() / 2f, 0f);
            beeController.movePoint.position = b.transform.position;

        }
    }

    public void spawnBees(int count)
    {
        for(var i = 0; i < count; i++)
        {
            GameObject tempBee = GameObject.Instantiate(beePrefab, new Vector3(0f, 0f, 0f), transform.rotation);
            List<GameObject> allBees = beeManager.GetComponent<BeeManager>().allBees;
            tempBee.transform.SetParent(beeManager.transform);
            tempBee.transform.localScale = new Vector3(1f, 1f, 1f);
            tempBee.transform.localPosition = new Vector3(0f, 0f, 0f);
            allBees.Add(tempBee.transform.Find("BeeObj").gameObject);
            int randomY = Random.Range(mapManager.floorCoard[1], mapManager.floorCoard[3] + 1);

            BeeController beeController = tempBee.transform.Find("BeeObj").GetComponent<BeeController>();
            beeController.addTo(tileInstances[new Vector3Int(mapManager.floorCoard[2], randomY, 0)]);
            beeController.setYPos(randomY);
            beeController.setXPos(mapManager.floorCoard[2]);
            //beeController.yPos = randomY;
            //beeController.xPos = mapManager.floorCoard[2];
        }
    }

    private void Update()
    {
        
    }
}
