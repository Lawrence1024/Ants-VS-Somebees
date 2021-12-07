using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AntShop : MonoBehaviour
{
    public List<CardController> cardControllers;
    public AntManager antManager;
    public GameState gameState;

    private GameObject foodTextObj;
    // Start is called before the first frame update
    void Start()
    {
        foodTextObj = GameObject.Find("FoodCountText");
        foodTextObj.GetComponent<TMPro.TextMeshProUGUI>().text = gameState.food.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        foodTextObj.GetComponent<TMPro.TextMeshProUGUI>().text = gameState.food.ToString();
    }
    public void placeAnt(CardController selectedCard)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = antManager.map.WorldToCell(mousePosition);
        Dictionary<Vector3Int, TileData> tileInstances = antManager.tileInstances;
        if (tileInstances.ContainsKey(gridPosition) && !(tileInstances[gridPosition].isAntHive || tileInstances[gridPosition].isBeeHive))
        {
            antManager.placeAnt(selectedCard, gridPosition);
        }
    }
}
