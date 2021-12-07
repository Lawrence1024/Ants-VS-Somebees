using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeData : InsectData
{
    public float moveSpeed = 3f;
    public LayerMask whatStopsMovement;
    //public LayerMask boxLayer;
    public LayerMask perimeterLayer;
    //public ArrayList movementHistory = new ArrayList();
    public List<int> startingPosition;
    public Vector3 startingVectPosition;
    //public List<List<int>> positionHistory = new List<List<int>>();
    //private PiecePosition piecePosition;
    public string attemptMovement;
    public bool canMove = true;
    public bool movePointCloseEnough;
    public GameObject gameCanvas;
    public Transform movePoint;
    public BeeData(Grid groundMap) : base(groundMap)
    {
        
    }
}
