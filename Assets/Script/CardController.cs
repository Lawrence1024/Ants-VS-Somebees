using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public float foodCost = 1;
    public string cardName;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 originalPosition;
    private GameObject iconDragged;
    private GameObject cardBase;
    private GameObject icon;
    private GameObject priceTag;
    private GameObject nameTag;
    public AntShop antShop;
    public GameObject antPrefab;
    public GameState gameState;
    private AntController antController;


    // Start is called before the first frame update
    void Start()
    {
        iconDragged = transform.Find("IconDragged").gameObject;
        cardBase = transform.Find("Base").gameObject;
        icon = transform.Find("Icon").gameObject;
        priceTag = transform.Find("PriceTag").gameObject;
        nameTag = transform.Find("CardName").gameObject;
        originalPosition = iconDragged.transform.position;
        iconDragged.SetActive(false);
        antShop = transform.parent.GetComponent<AntShop>();
        gameState = antShop.gameState;
        icon.GetComponent<SpriteRenderer>().sprite = antPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
        iconDragged.GetComponent<SpriteRenderer>().sprite = antPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
        GameObject temp = Instantiate(antPrefab, new Vector3(0f, 0f, 0f), transform.rotation);
        foodCost = temp.GetComponentInChildren<AntController>().foodCost;
        cardName = temp.GetComponentInChildren<AntController>().antName;
        Destroy(temp);
        priceTag.GetComponent<TMPro.TextMeshProUGUI>().text = foodCost.ToString();
        nameTag.GetComponent<TMPro.TextMeshProUGUI>().text = cardName.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.food < foodCost)
        {
            changeOpacity(0.5f);
        }
        else
        {
            changeOpacity(1f);
        }
    }

    private void changeOpacity(float opacity)
    {
        foreach (var sprite in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            Color originalColor = sprite.color;
            sprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, opacity);
        }
        foreach (var text in gameObject.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
        {
            Color originalColor = text.color;
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, opacity);
        }
    }

    private void OnMouseDown()
    {
        if(gameState.food >= foodCost)
        {
            offset = iconDragged.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            offset = new Vector3(0f, 0f, offset.z);
            iconDragged.SetActive(true);
            changeCardOpacity(0.5f);
        }
        else
        {
            Debug.Log("Not enough food: You currently have " + gameState.food);
        }
    }
    private void OnMouseDrag()
    {
        if (gameState.food >= foodCost)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            iconDragged.transform.position = curPosition;
        }
    }
    private void changeCardOpacity(float opacity)
    {
        Color cardBaseColor = cardBase.GetComponent<Renderer>().material.color;
        cardBase.GetComponent<Renderer>().material.color = new Color(cardBaseColor.r, cardBaseColor.g, cardBaseColor.b, opacity);
        Color iconColor = icon.GetComponent<Renderer>().material.color;
        icon.GetComponent<Renderer>().material.color = new Color(iconColor.r, iconColor.g, iconColor.b, opacity);

    }
    private void OnMouseUp()
    {
        if (gameState.food >= foodCost)
        {
            iconDragged.transform.position = originalPosition;
            iconDragged.SetActive(false);
            changeCardOpacity(1f);
            antShop.placeAnt(this);
        }
    }
}
