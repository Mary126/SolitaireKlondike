using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void shuffleGOList(List<Sprite> inputList)
    {
        System.Random random = new System.Random();
        for (int i = inputList.Count - 1; i >= 1; i--)
        {
            int j = random.Next(i + 1);
            // обменять значения data[j] и data[i]
            var temp = inputList[j];
            inputList[j] = inputList[i];
            inputList[i] = temp;
        }
    }
    

    public void GenerateDeck()
    {
        shuffleGOList(instances.cards); //shuffle existing sprites
        for (int row = 7; row >= 1; row--) // generate 7 bottom rows
        {
            for (int i = 1; i <= row; i++)
            {
                Sprite spr = instances.cards[instances.cards.Count - 1];
                if (instances.cards.Count > 0)
                {
                    string cardTag = "BottomRow" + row.ToString();
                    GameObject card = Instantiate(instances.card);
                    CardInstances cardInstances = card.GetComponent<CardInstances>();
                    card.transform.SetParent(instances.cardPlaces.transform);
                    card.tag = instances.field[cardTag][instances.field[cardTag].Count - 1].tag;
                    Vector3 positionToPlace = instances.field[cardTag][instances.field[cardTag].Count - 1].transform.position;
                    positionToPlace.y -= 0.3f;
                    positionToPlace.z -= 1;
                    card.transform.position = positionToPlace;
                    card.GetComponent<CardController>().klondikeRules = klondikeRules;
                    card.GetComponent<CardController>().instances = instances;
                    cardInstances.SetCardSuit(spr);
                    card.name = "card" + "bottomrow"+cardInstances.cardSuit.suit+cardInstances.cardSuit.number;
                    cardInstances.SetPosition("BottomRow", row);
                    if (i == row) // if the card is the last one open that card
                    {
                        card.GetComponent<SpriteRenderer>().sprite = 
                            cardInstances.cardSuit.sprite;
                        cardInstances.isOpen = true;
                    }
                    instances.cards.RemoveAt(instances.cards.Count - 1);
                    instances.field[cardTag].Add(card);
                }
            }
        }
    }
}
