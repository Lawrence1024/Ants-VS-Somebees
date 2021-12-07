using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject peaPrefab;
    //[SerializeField]
    //private GameObject beeManager;

    public AntController antController;

    //public MapManager mapManager;
    private Dictionary<Vector3Int, TileData> tileInstances;
    void Start()
    {
        init();
    }

    private void init()
    {
        
    }

    public void spawnPeas(int count)
    {
        for(int i=0; i<count; i++)
        {
            GameObject tempPea = GameObject.Instantiate(peaPrefab, new Vector3(0f, 0f, 0f), transform.rotation);
            antController = transform.parent.Find("AntObj").GetComponent<ThrowerAntController>();
            tempPea.GetComponent<PeaController>().antController = antController;
            tempPea.transform.SetParent(antController.transform);
            tempPea.transform.localScale = new Vector3(1f, 1f, 1f);
            tempPea.transform.localPosition = new Vector3(0f, 0f, 0f);
            Vector3 finalPos = new Vector3(antController.transform.position.x, antController.transform.position.y, 0f);
            tempPea.transform.position = finalPos;
            antController.peas.Add(tempPea.gameObject);
        }
    }

    private void Update()
    {

    }
}
