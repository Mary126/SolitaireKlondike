using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instances : MonoBehaviour
{
    [System.NonSerialized] public List<Sprite> topDeck;
    [System.NonSerialized] public List<GameObject> topDeckOpened;
    [System.NonSerialized] public GameController gameController;
    [System.NonSerialized] public List<List<GameObject>> bottomRow;
    public List<Sprite> cards;
    public GameObject card;
    public Sprite CardBack;
    public Sprite CardBlank;
    public Vector3 topDeckPosition;
    public Vector3 topDeckOpenPosition;
    public List<GameObject> bottomRowPlaces;
    
    void Awake()
    {
        gameController = GetComponent<GameController>();
        topDeckOpened = new List<GameObject>();
        bottomRow = new List<List<GameObject>>() { 
            new List<GameObject>(), 
            new List<GameObject>(), 
            new List<GameObject>(), 
            new List<GameObject>(), 
            new List<GameObject>(), 
            new List<GameObject>() 
        };
        for (int i = 0; i < 6; i++)
        {
            bottomRow[i].Insert(0, bottomRowPlaces[i]); // make the first element of the column
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
