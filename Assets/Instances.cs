using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Instances : MonoBehaviour
{
    [System.NonSerialized] public List<Sprite> topDeck;
    [System.NonSerialized] public GameController gameController;
    [System.NonSerialized] public Dictionary<string, List<GameObject>> field;
    public List<Sprite> cards;
    public GameObject card;
    public Sprite CardBack;
    public Sprite CardBlank;
    public Vector3 topDeckPosition;
    public Vector3 topDeckOpenPosition;
    public List<GameObject> bottomRowPlaces;
    public List<GameObject> topRowPlaces;

    public void GenerateDeck()
    {
        System.Random rand = new System.Random(System.DateTime.Now.Millisecond);
        topDeck = cards.OrderBy(x => rand.Next()).ToList(); //shuffle existing sprites
    }
    void Awake()
    {
        gameController = GetComponent<GameController>();
        field = new Dictionary<string, List<GameObject>>();
        field.Add("DeckOpened", new List<GameObject>());
        field.Add("TopRow1", new List<GameObject>());
        field.Add("TopRow2", new List<GameObject>());
        field.Add("TopRow3", new List<GameObject>());
        field.Add("TopRow4", new List<GameObject>());
        field.Add("BottomRow1", new List<GameObject>());
        field.Add("BottomRow2", new List<GameObject>());
        field.Add("BottomRow3", new List<GameObject>());
        field.Add("BottomRow4", new List<GameObject>());
        field.Add("BottomRow5", new List<GameObject>());
        field.Add("BottomRow6", new List<GameObject>());
        for (int i = 0; i < 4; i++)
        {
            string index = "TopRow" + (i + 1).ToString();
            field[index].Insert(0, topRowPlaces[i]); // make the first element of the column
        }
        for (int i = 0; i < 6; i++)
        {
            string index = "BottomRow" + (i+1).ToString();
            field[index].Insert(0, bottomRowPlaces[i]); // make the first element of the column
        }
        GenerateDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
