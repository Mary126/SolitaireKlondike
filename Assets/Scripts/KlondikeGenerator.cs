using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KlondikeGenerator : MonoBehaviour
{
    private Instances instances;
    private KlondikeRules klondikeRules;
    // Start is called before the first frame update
    void Awake()
    {
        instances = GetComponent<Instances>();
        klondikeRules = GetComponent<KlondikeRules>();
    }
    public void GenerateDeck()
    {
        System.Random rand = new System.Random(System.DateTime.Now.Millisecond);
        instances.cards = instances.cards.OrderBy(x => rand.Next()).ToList(); //shuffle existing sprites
        for (int row = 7; row >= 1; row--)
        {
            for (int i = 1; i <= row; i++)
            {
                if (instances.cards.Count > 0)
                {
                    GameObject card = Instantiate(instances.card);
                    card.transform.SetParent(instances.cardPlaces.transform);
                    instances.cards.RemoveAt(instances.cards.Count - 1);
                    Debug.Log(instances.cards.Count);
                    card.tag = instances.field["BottomRow" + row.ToString()][instances.field["BottomRow" + row.ToString()].Count - 1].tag;
                    Vector3 positionToPlace = instances.field["BottomRow" + row.ToString()][instances.field["BottomRow" + row.ToString()].Count - 1].transform.position;
                    positionToPlace.y -= 0.3f;
                    positionToPlace.z -= 1;
                    card.transform.position = positionToPlace;
                    card.GetComponent<CardController>().klondikeRules = klondikeRules;
                    card.GetComponent<CardController>().instances = instances;
                    card.GetComponent<CardInstances>().SetCardType(instances.cards[instances.cards.Count - 1]);
                    card.GetComponent<CardInstances>().SetPosition("BottomRow", row);
                    if (i == row)
                    {
                        card.GetComponent<SpriteRenderer>().sprite = card.GetComponent<CardInstances>().info.sprite;
                        card.GetComponent<CardInstances>().isOpen = true;
                    }
                    instances.field["BottomRow" + row.ToString()].Add(card);
                }
            }
        }
        instances.topDeck = instances.cards;
        
    }
}
